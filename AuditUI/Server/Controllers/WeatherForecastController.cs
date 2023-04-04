using Audit.Core;
using Audit.EntityFramework;
using Audit.MongoDB.Providers;

using Microsoft.AspNetCore.Mvc;

namespace AuditUI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<AuditEvent> Get([FromQuery] AuditFilterForm filter)
        {

            var query = ((MongoDataProvider)Audit.Core.Configuration.DataProvider).QueryEvents<AuditEventEntityFramework>();

            if (filter.Entity != null)
            {
                query = query.Where(q => q.EntityFrameworkEvent.Entries.Any(c => c.Table == filter.Entity));
            }

            var list = query.ToList();


            return list;
        }

        [HttpGet("all")]
        public IEnumerable<string> Gets()
        {

            var list = ((MongoDataProvider)Audit.Core.Configuration.DataProvider).QueryEvents<AuditEventEntityFramework>().ToList();

            List<string> result = new List<string>();

            foreach (AuditEventEntityFramework item in list)
            {
                foreach (var item2 in item.EntityFrameworkEvent.Entries)
                {
                    result.Add(item2.Table);
                }
            }

            return result.OrderBy(c => c).Distinct();
        }
    }

    public class AuditFilterForm
    {
        public string? UserName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }


        public string? Entity { get; set; }

    }
}