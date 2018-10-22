using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System.Threading.Tasks;
using JCampon.MongoDB.Repositories;

namespace JCampon.MongoDB.Tests
{
	public class TestsForSampleRepositoryWithObjectId : BaseTestsForMongoDbRepositories
	{
		private const string TheTestCollectionName = "TestCollectionForEntityWithObjectId";
		protected SampleRepositoryWithObjectId TheSampleRepositoryWithObjectId;

	    [OneTimeSetUp]
	    public void OneTimeSetUp()
	    {
	        Initialise();
	    }

        [Test]
	    public void Validate_that_the_sample_repository_implements_the_expected_inheritance_and_interfaces()
	    {
	        Assert.That(TheSampleRepositoryWithObjectId, Is.InstanceOf<MongoDbRepositoryWithObjectId<SampleEntity>>());   // Inherits from base class
	        Assert.That(TheSampleRepositoryWithObjectId, Is.InstanceOf<BaseMongoDbRepository<SampleEntity, ObjectId>>()); // Inherits from root base class
            Assert.That(TheSampleRepositoryWithObjectId is IBaseMongoDbRepository<SampleEntity, ObjectId>, Is.True);      // Implements interface through base classes
        }

        [Test]
		public void Validate_the_collection_name_for_the_repository()
		{
			Assert.That(TheSampleRepositoryWithObjectId.CollectionName, Is.EqualTo(TheTestCollectionName));
		}

		[Test]
		public async Task Test_that_a_record_can_be_added_into_the_collection()
		{
			var newEntity = GetNewEntity();

			var theNewIdReturned = await TheSampleRepositoryWithObjectId.AddOneAsync(newEntity);

			Assert.That(theNewIdReturned, Is.Not.Null);
			Assert.That(theNewIdReturned, Is.TypeOf<ObjectId>());
		}

		[Test]
		public async Task Test_that_a_record_can_be_read_from_the_collection()
		{
			var newEntity = GetNewEntity();
			var theNewIdReturned = await TheSampleRepositoryWithObjectId.AddOneAsync(newEntity);

			var theEntityReadFromTheDatabase = await TheSampleRepositoryWithObjectId.GetOneByIdAsync(theNewIdReturned);

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
			var theNewIdReturned = await TheSampleRepositoryWithObjectId.AddOneAsync(newEntity);
			var theEntityReadFromTheDatabase = await TheSampleRepositoryWithObjectId.GetOneByIdAsync(theNewIdReturned);

			// Act
			theEntityReadFromTheDatabase.EntityName = "Edited name";
			var theReplaceOneResult = await TheSampleRepositoryWithObjectId.FindOneAndReplaceAsync(theEntityReadFromTheDatabase);
			var theUpdatedEntityReadFromTheDatabase = await TheSampleRepositoryWithObjectId.GetOneByIdAsync(theNewIdReturned);

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
			var theNewIdReturned = await TheSampleRepositoryWithObjectId.AddOneAsync(newEntity);
			var theEntityReadFromTheDatabase = await TheSampleRepositoryWithObjectId.GetOneByIdAsync(theNewIdReturned);

			// Act
			var actualDeleteResult = await TheSampleRepositoryWithObjectId.DeleteOneAsync(theEntityReadFromTheDatabase);
			var searchResultAfterDeletion = await TheSampleRepositoryWithObjectId.GetOneByIdAsync(theNewIdReturned);

			// Assert
			Assert.That(actualDeleteResult.IsAcknowledged, Is.True);
			Assert.That(actualDeleteResult.DeletedCount, Is.EqualTo(1));
			Assert.That(searchResultAfterDeletion, Is.Null);		}

	    #region Private helper methods

	    private void Initialise()
	    {
	        DoInitialCleanUpOfCollections(TheTestCollectionName);

	        TheSampleRepositoryWithObjectId = new SampleRepositoryWithObjectId(DbContext, TheTestCollectionName);
	    }

	    private SampleEntity GetNewEntity()
	    {
	        return new SampleEntity() {EntityName = "New Entity with OBJECT_ID Id"};
	    }

	    #endregion
	}
}