using Audit.Core;
using Audit.MongoDB.Providers;

using MongoDB.Driver;

namespace AuditTestApi
{

    public class AuditService
    {
        private IMongoDatabase database { get; set; }

        public AuditService()
        {

            string connectionString = "mongodb://marketing:Marketing2019!@157.90.29.241:27017";
            MongoClient client = new MongoClient(connectionString);

            // Veritabanı ve koleksiyon seçimi
            database = client.GetDatabase("Audit");
        }

        public async Task<List<AuditEvent>> GetList()

        {



            var list = ((MongoDataProvider)Audit.Core.Configuration.DataProvider).QueryEvents().ToList();


            return list;

        }
    }

}
