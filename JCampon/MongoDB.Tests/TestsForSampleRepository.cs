using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System.Threading.Tasks;

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
		public async Task Test_that_a_record_can_be_added_into_the_collection()
		{
			var newEntity = GetNewEntity();

			var theNewIdReturned = await TheSampleRepository.AddOneAsync(newEntity);

			Assert.That(theNewIdReturned, Is.Not.Null);
			Assert.That(theNewIdReturned, Is.TypeOf<ObjectId>());
		}

		[Test]
		public async Task Test_that_a_record_can_be_read_from_the_collection()
		{
			var newEntity = GetNewEntity();
			var theNewIdReturned = await TheSampleRepository.AddOneAsync(newEntity);

			var theEntityReadFromTheDatabase = await TheSampleRepository.GetByIdAsync(theNewIdReturned);

			Assert.That(theEntityReadFromTheDatabase, Is.Not.Null);
			Assert.That(theEntityReadFromTheDatabase, Is.TypeOf<SampleEntity>());
			Assert.That(theEntityReadFromTheDatabase.Id, Is.EqualTo(theNewIdReturned));
			Assert.That(theEntityReadFromTheDatabase.EntityName, Is.EqualTo(newEntity.EntityName));
		}

		[Test]
		public async Task Test_that_a_record_can_be_edited_on_the_collection()
		{
			// Arrange
			var newEntity = GetNewEntity();
			var theNewIdReturned = await TheSampleRepository.AddOneAsync(newEntity);
			var theEntityReadFromTheDatabase = await TheSampleRepository.GetByIdAsync(theNewIdReturned);

			// Act
			theEntityReadFromTheDatabase.EntityName = "Edited name";
			var theReplaceOneResult = await TheSampleRepository.FindOneAndReplaceAsync(theEntityReadFromTheDatabase);
			var theUpdatedEntityReadFromTheDatabase = await TheSampleRepository.GetByIdAsync(theNewIdReturned);

			// Assert
			Assert.That(theReplaceOneResult, Is.Not.Null);
			Assert.That(theUpdatedEntityReadFromTheDatabase, Is.Not.Null);
			Assert.That(theUpdatedEntityReadFromTheDatabase.Id, Is.EqualTo(theEntityReadFromTheDatabase.Id));
			Assert.That(theUpdatedEntityReadFromTheDatabase.EntityName, Is.EqualTo("Edited name"));
		}

		[Test]
		public async Task Test_that_a_record_can_be_deleted_from_the_collection()
		{
			// Arrange
			var newEntity = GetNewEntity();
			var theNewIdReturned = await TheSampleRepository.AddOneAsync(newEntity);
			var theEntityReadFromTheDatabase = await TheSampleRepository.GetByIdAsync(theNewIdReturned);

			// Act
			var actualDeleteResult = await TheSampleRepository.DeleteOneAsync(theEntityReadFromTheDatabase);
			var searchResultAfterDeletion = await TheSampleRepository.GetByIdAsync(theNewIdReturned);

			// Assert
			Assert.That(actualDeleteResult.IsAcknowledged, Is.True);
			Assert.That(actualDeleteResult.DeletedCount, Is.EqualTo(1));
			Assert.That(searchResultAfterDeletion, Is.Null);		}

		private SampleEntity GetNewEntity()
		{
			return new SampleEntity() {EntityName = "New Entity"};
		}
	}
}