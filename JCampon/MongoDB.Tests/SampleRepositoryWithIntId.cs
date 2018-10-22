using JCampon.MongoDB.Repositories;

namespace JCampon.MongoDB.Tests
{
    public class SampleRepositoryWithIntId : MongoDbRepositoryWithIntId<SampleEntityWithIntId>
    {
        public SampleRepositoryWithIntId(IMongoDbDatabaseContext dbContext, string collectionName) : base(dbContext, collectionName)
        {
        }
    }
}