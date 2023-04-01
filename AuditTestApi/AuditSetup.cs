
using Audit.Core;
using Audit.DynamicProxy;
using Audit.WebApi;

using Microsoft.AspNetCore.Mvc;

namespace AuditTestApi
{
    public static class AuditSetup
    {

        public static IServiceCollection AddAuditedTransient<TService, TImplementation>(this IServiceCollection services)
           where TService : class
           where TImplementation : class, TService
        {
            return services.AddTransient<TService>(s =>
            {
                var svc = (TService)ActivatorUtilities.CreateInstance<TImplementation>(s);
                return AuditProxy.Create(svc, new InterceptionSettings()
                {
                    EventType = "servicelogs"
                });
            });
        }

        public static MvcOptions AuditSetupFilter(this MvcOptions mvcOptions)
        {
            // Add the global MVC Action Filter to the filter chain
            mvcOptions.AddAuditFilter(a => a
                .LogAllActions()
                .WithEventType("mvc")
                .IncludeModelState()
                .IncludeRequestBody()
                .IncludeResponseBody());

            return mvcOptions;
        }

        public static void AuditSetupMiddleware(this IApplicationBuilder app)
        {
            // Add the audit Middleware to the pipeline
            app.UseAuditMiddleware(_ => _
                .FilterByRequest(r => !r.Path.Value!.EndsWith("favicon.ico"))
                .WithEventType("http")
                .IncludeHeaders()
                .IncludeRequestBody()
                .IncludeResponseBody());
        }

        public static void AuditSetupOutput(this WebApplication app)
        {
            // TODO: Configure the audit output.
            // For more info, see https://github.com/thepirat000/Audit.NET#data-providers.
            Audit.Core.Configuration.Setup()
                .UseFileLogProvider(_ => _
                    .Directory(@"C:\Logs")
                    .FilenameBuilder(ev => $"{ev.StartDate:yyyyMMddHHmmssffff}_{ev.EventType}.json"));

            Audit.Core.Configuration.JsonSettings.WriteIndented = true;

            // Include the trace identifier in the audit events
            var httpContextAccessor = app.Services.GetRequiredService<IHttpContextAccessor>();
            Audit.Core.Configuration.AddCustomAction(ActionType.OnScopeCreated, scope =>
            {
                scope.SetCustomField("TraceId", httpContextAccessor.HttpContext?.TraceIdentifier);
            });
        }
    }
}
