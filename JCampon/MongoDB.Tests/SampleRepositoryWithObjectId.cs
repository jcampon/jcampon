using JCampon.MongoDB.Repositories;

namespace JCampon.MongoDB.Tests
{
	public class SampleRepositoryWithObjectId : MongoDbRepositoryWithObjectId<SampleEntity>
    {
		public SampleRepositoryWithObjectId(IMongoDbDatabaseContext dbContext, string collectionName) : base(dbContext, collectionName)
		{
		}
	}
}
