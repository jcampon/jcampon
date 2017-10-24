using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCampon.MongoDB.Repositories;
using NUnit.Framework;

namespace JCampon.MongoDB.Tests
{
	[TestFixture]
	public class TestsMongoDbRepositories
	{
		private SampleRepository _sampleRepository;

		[SetUp]
		public void Initialise()
		{
			var dbContext = new MongoDbDatabaseContext("mongodb://localhost:27017", "TestDatabase");
			_sampleRepository = new SampleRepository(dbContext, "TestCollection");
		}
	}
}
