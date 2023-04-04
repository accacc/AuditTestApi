using Audit.Core;
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
        public IEnumerable<AuditEvent> Get()
        {

            var list = ((MongoDataProvider)Configuration.DataProvider).QueryEvents().ToList();


            return list;
        }
    }
}