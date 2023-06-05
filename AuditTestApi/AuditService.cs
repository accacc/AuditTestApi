using Audit.Core;
using Audit.EntityFramework;
using Audit.MongoDB.Providers;
using Elasticsearch.Net;
using MongoDB.Driver;
using Nest;
using AuditEvent = Audit.Core.AuditEvent;

namespace AuditTestApi
{


	public class ConvertAudit
	{
		public static void Convert(AuditEventEntityFramework auditEventEntityFramework)
		{
			
			AuditUI.Shared.EventAudit eventAudit = new AuditUI.Shared.EventAudit();



		}

	}

	public class AuditService
    {
		//private IMongoDatabase database { get; set; }
		private readonly ElasticClient client;






		public AuditService()
        {

			//string connectionString = "mongodb://marketing:Marketing2019!@157.90.29.241:27017";
			//MongoClient client = new MongoClient(connectionString);

			//// Veritabanı ve koleksiyon seçimi
			//database = client.GetDatabase("Audit");

			var pool = new SingleNodeConnectionPool(new Uri("https://search-globalcatalog-sonzlm2rcljjvdrznzhyuyzmxa.us-west-2.es.amazonaws.com/"));

			var settings = new ConnectionSettings(pool);

			settings.BasicAuthentication("gcuser", "gc-196!1-usR");

			client = new ElasticClient(settings);
		}

        public async Task<List<AuditEvent>> GetList()

        {
			QueryContainer query = new QueryContainerDescriptor<AuditEvent>();

			//query = query &&
			//		 new QueryContainerDescriptor<AuditEvent>()
			//		 .MatchPhrasePrefix(c => c
			//			  .Field(p => p.Id)
			//			  .Query(id)
			//		  );

			var response = await client.SearchAsync<AuditEvent>(s => s.Skip(0)
			.Query(q => query)
			.Index(typeof(AuditEvent).Name.ToLower()));

			if (!response.IsValid)
			{
				throw new Exception(response.DebugInformation);
			}


			return response.Documents.ToList();

        }
    }

}
