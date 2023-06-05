using AuditTestApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(mvc =>
{
    //mvc.AuditSetupFilter();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAuditedTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductService, ProductService>();
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

//Audit.Core.Configuration.AddOnSavingAction(scope =>
//{
//    var interceptEvent = scope.GetAuditInterceptEvent();

//    if (interceptEvent != null)
//    {
//        foreach (var arg in interceptEvent.Arguments)
//        {
//            arg.Value = ToJson(arg.Value);
//        }

//        interceptEvent.Result.Value = ToJson(interceptEvent.Result.Value);
//    }
//});

//List<Uri> elasticSearchPoolUris = new List<Uri>();

//elasticSearchPoolUris.Add(new Uri("https://search-globalcatalog-sonzlm2rcljjvdrznzhyuyzmxa.us-west-2.es.amazonaws.com/"));


//var pool = new StaticConnectionPool(elasticSearchPoolUris);

//var settings = new Nest.ConnectionSettings(pool);

//settings.BasicAuthentication("gcuser", "gc-196!1-usR");

//var auditingConnectionSettings = new AuditConnectionSettings(pool);

//Audit.Core.Configuration.Setup()
//              .UseElasticsearch(config => config
//              .ConnectionSettings(auditingConnectionSettings)
//              .Index(auditEvent => auditEvent.EventType)
//              .Id(ev => Guid.NewGuid()));


Audit.Core.Configuration.Setup()
	.UseCustomProvider(new ArbimedAsyncAuditProvider(builder.Configuration));

//Audit.Core.Configuration.Setup()
//    .UseMongoDB(config => config
//        .ConnectionString("mongodb://marketing:Marketing2019!@157.90.29.241:27017")
//        .Database("Audit")
//        .Collection("Event"));


//BsonClassMap.RegisterClassMap<AuditTestApi.AuditEvent>(cm =>
//{
//    cm.AutoMap();
//    cm.MapIdMember(c => c.Id).SetIdGenerator(CombGuidGenerator.Instance);
//});

//app.AuditSetupMiddleware();

//app.AuditSetupOutput();

app.Run();


static string ToJson(object obj)
{
    return obj == null ? null : System.Text.Json.JsonSerializer.Serialize(obj, Audit.Core.Configuration.JsonSettings);
}
