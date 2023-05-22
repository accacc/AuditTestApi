using Audit.EntityFramework;
using Audit.MongoDB.Providers;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using AuditUI.Client;
using MongoDB.Bson.Serialization;

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
        public async Task<IEnumerable<AuditEventEntityFramework>> Get([FromQuery] AuditFilterForm filter)
        {

            await Task.Delay(1);

            BsonClassMap.RegisterClassMap<EventAudit>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            try
            {

                var fields = Builders<EventAudit>.Projection.Exclude("_id");

                List<EventAudit>  result = new MongoClient("mongodb://marketing:Marketing2019!@157.90.29.241:27017").GetDatabase("Audit").GetCollection<EventAudit>("Event").Find(_=> true).Project<EventAudit>(fields).ToList();

                var query = ((MongoDataProvider)Audit.Core.Configuration.DataProvider).QueryEvents<AuditEventEntityFramework>();

                if (filter.Entity != null)
                {
                    query = query.Where(q => q.EntityFrameworkEvent.Entries.Any(c => c.Table == filter.Entity));
                }

                if (filter.SearchText != null)
                {
                    query = query.Where(q => q.EntityFrameworkEvent.Entries.Any(c => c.Changes.Any(c => c.ColumnName.Contains(filter.SearchText))));
                }

                if (filter.SearchText != null)
                {
                    query = query.Where(q => q.EntityFrameworkEvent.Entries.Any(c => c.ColumnValues.Any(c => c.Key == filter.SearchText)));
                }

                var list = query.ToList();

                return list;
            }
            catch (Exception ex)
            {

                throw;
            }


            
        }

        [HttpGet("all")]
        public async Task<IEnumerable<string>> Gets()
        {
            await Task.Delay(1);

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

        public string? SearchText { get; set; }
        public string? Entity { get; set; }

    }
}