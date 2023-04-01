using Audit.DynamicProxy;

using AuditTestApi;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(mvc =>
{
    mvc.AuditSetupFilter();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuditedTransient<IProductService, ProductService>();
builder.Services.AddDbContext<ProductDbContext>(_ => _.UseInMemoryDatabase("default"));
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();



Audit.EntityFramework.Configuration.Setup()
               .ForContext<ProductDbContext>(_ => _
                   .AuditEventType("audit")
                   .IncludeEntityObjects())
               .UseOptOut();


//Audit.Core.Configuration.Setup()
//    .UseDynamicAsyncProvider(config => config
//        .OnInsert(async ev =>

//        await File.WriteAllTextAsync(@"C:\temp\a.json", ev.ToJson())));

Audit.Core.Configuration.AddOnSavingAction(scope =>
{
    var interceptEvent = scope.GetAuditInterceptEvent();

    foreach (var arg in interceptEvent.Arguments)
    {
        arg.Value = ToJson(arg.Value);
    }

    interceptEvent.Result.Value = ToJson(interceptEvent.Result.Value);
});





//Audit.Core.Configuration.Setup()
//              .UseElasticsearch(config => config
//              .ConnectionSettings(new Uri("http://localhost:9200"))
//              .Index(auditEvent => auditEvent.EventType)
//              .Id(ev => Guid.NewGuid()));


app.AuditSetupMiddleware();

app.AuditSetupOutput();

app.Run();


static string ToJson(object obj)
{
    return obj == null ? null : System.Text.Json.JsonSerializer.Serialize(obj, Audit.Core.Configuration.JsonSettings);
}
