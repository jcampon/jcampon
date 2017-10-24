using NUnit.Framework;

namespace JCampon.MongoDB.Tests
{
	public class TestsForSampleRepository : BaseTestsForMongoDbRepositories
	{
		private const string TheExpectedCollectionName = "TestCollectionForJCamponTestDatabase";
		protected SampleRepository TheSampleRepository;

		[SetUp]
		public void Initialise()
		{
			TheSampleRepository = new SampleRepository(DbContext, TheExpectedCollectionName);
		}

		[Test]
		public void Validate_the_collection_name_for_the_repository()
		{
			Assert.That(TheSampleRepository.CollectionName, Is.EqualTo(TheExpectedCollectionName));
		}
	}
}