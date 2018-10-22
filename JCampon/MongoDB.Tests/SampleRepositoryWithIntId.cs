using JCampon.MongoDB.Repositories;

namespace JCampon.MongoDB.Tests
{
    public class SampleRepositoryWithIntId : MongoDbRepositoryWithIntegerId<SampleEntityWithIntId>
    {
        public SampleRepositoryWithIntId(IMongoDbDatabaseContext dbContext, string collectionName) : base(dbContext, collectionName)
        {
        }
    }
}