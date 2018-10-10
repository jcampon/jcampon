using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCampon.MongoDB.Repositories;

namespace JCampon.MongoDB.Tests
{
	public class SampleRepository : MongoDbRepositoryWithObjectId<SampleEntity>
    {
		public SampleRepository(IMongoDbDatabaseContext dbContext, string collectionName) : base(dbContext, collectionName)
		{
		}
	}
}
