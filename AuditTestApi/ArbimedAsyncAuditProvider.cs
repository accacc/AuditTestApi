using Audit.Core;
using Elasticsearch.Net;
using Nest;
using AuditEvent = Audit.Core.AuditEvent;

namespace AuditTestApi
{
	public class ArbimedAsyncAuditProvider : AuditDataProvider
	{
		private readonly IConfiguration config;
		private readonly ElasticClient client;
		public ArbimedAsyncAuditProvider(IConfiguration _config)
		{
			config = _config;

			var pool = new SingleNodeConnectionPool(new Uri("https://search-globalcatalog-sonzlm2rcljjvdrznzhyuyzmxa.us-west-2.es.amazonaws.com/"));

			var settings = new ConnectionSettings(pool);

			settings.BasicAuthentication("gcuser", "gc-196!1-usR");

			client = new ElasticClient(settings);
		}

		public override object InsertEvent(AuditEvent auditEvent)
		{
			var bulkResponse = client.Index<AuditEvent>(auditEvent, d => d.Index(typeof(AuditEvent).Name.ToLower()));
			return bulkResponse;
		}


		public override Task<object> InsertEventAsync(AuditEvent auditEvent)
		{
			return Task.FromResult(InsertEvent(auditEvent));
		}

		public override void ReplaceEvent(object eventId, AuditEvent auditEvent)
		{
		}

		public override Task ReplaceEventAsync(object eventId, AuditEvent auditEvent)
		{
			ReplaceEvent(eventId, auditEvent);

			return Task.CompletedTask;
		}
	}
}
