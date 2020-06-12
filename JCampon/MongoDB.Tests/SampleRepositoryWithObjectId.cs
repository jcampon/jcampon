using JCampon.MongoDB.Repositories;
using MongoDB.Driver;

namespace JCampon.MongoDB.Tests
{
	public class SampleRepositoryWithObjectId : MongoDbRepositoryWithObjectId<SampleEntity>
    {
		public SampleRepositoryWithObjectId(IMongoCollection<SampleEntity> collection) : base(collection)
		{
		}
	}
}
