using JCampon.MongoDB.Repositories;
using MongoDB.Driver;

namespace JCampon.MongoDB.Tests
{
    public class SampleRepositoryWithIntId : MongoDbRepositoryWithIntegerId<SampleEntityWithIntId>
    {
        public SampleRepositoryWithIntId(IMongoCollection<SampleEntityWithIntId> collection) : base(collection)
        {
        }
    }
}