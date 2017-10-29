using MongoDB.Bson;
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

		[Test]
		public async void Test_that_a_record_can_be_added_into_the_collection()
		{
			var newEntity = GetNewEntity();

			var theNewIdReturned = await TheSampleRepository.Add(newEntity);

			Assert.That(theNewIdReturned, Is.Not.Null);
			Assert.That(theNewIdReturned, Is.TypeOf<ObjectId>());
		}

		[Test]
		public async void Test_that_a_record_can_be_read_from_the_collection()
		{
			var newEntity = GetNewEntity();
			var theNewIdReturned = await TheSampleRepository.Add(newEntity);

			var theEntityReadFromTheDatabase = await TheSampleRepository.GetById(theNewIdReturned);

			Assert.That(theEntityReadFromTheDatabase, Is.Not.Null);
			Assert.That(theEntityReadFromTheDatabase, Is.TypeOf<SampleEntity>());
			Assert.That(theEntityReadFromTheDatabase.Id, Is.EqualTo(theNewIdReturned));
			Assert.That(theEntityReadFromTheDatabase.EntityName, Is.EqualTo(newEntity.EntityName));
		}

		private SampleEntity GetNewEntity()
		{
			return new SampleEntity() {EntityName = "New Entity"};
		}
	}
}