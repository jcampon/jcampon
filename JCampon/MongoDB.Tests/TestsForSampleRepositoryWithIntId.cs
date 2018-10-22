using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System.Threading.Tasks;
using JCampon.MongoDB.Repositories;

namespace JCampon.MongoDB.Tests
{
	public class TestsForSampleRepositoryWithIntId : BaseTestsForMongoDbRepositories
	{
		private const string TheTestCollectionName = "TestCollectionForEntityWithIntId";
		protected SampleRepositoryWithIntId TheSampleRepositoryWithIntId;
        protected int IdValueCounter = 0;

	    [OneTimeSetUp]
	    public void OneTimeSetUp()
	    {
	        Initialise();
	    }

        [Test]
	    public void Validate_that_the_sample_repository_implements_the_expected_inheritance_and_interfaces()
	    {
	        Assert.That(TheSampleRepositoryWithIntId, Is.InstanceOf<MongoDbRepositoryWithIntId<SampleEntityWithIntId>>()); // Inherits from base class
	        Assert.That(TheSampleRepositoryWithIntId, Is.InstanceOf<BaseMongoDbRepository<SampleEntityWithIntId, int>>()); // Inherits from root base class
            Assert.That(TheSampleRepositoryWithIntId is IBaseMongoDbRepository<SampleEntityWithIntId, int>, Is.True);      // Implements interface through base classes
        }

        [Test]
		public void Validate_the_collection_name_for_the_repository()
		{
			Assert.That(TheSampleRepositoryWithIntId.CollectionName, Is.EqualTo(TheTestCollectionName));
		}

		[Test]
		public async Task Test_that_a_record_can_be_added_into_the_collection()
		{
			var newEntity = GetNewEntity();

			var theNewIdReturned = await TheSampleRepositoryWithIntId.AddOneAsync(newEntity);

			Assert.That(theNewIdReturned, Is.Not.Null);
			Assert.That(theNewIdReturned, Is.TypeOf<int>());
		}

		[Test]
		public async Task Test_that_a_record_can_be_read_from_the_collection()
		{
			var newEntity = GetNewEntity();
			var theNewIdReturned = await TheSampleRepositoryWithIntId.AddOneAsync(newEntity);

			var theEntityReadFromTheDatabase = await TheSampleRepositoryWithIntId.GetOneByIdAsync(theNewIdReturned);

			Assert.That(theEntityReadFromTheDatabase, Is.Not.Null);
			Assert.That(theEntityReadFromTheDatabase, Is.TypeOf<SampleEntityWithIntId>());
			Assert.That(theEntityReadFromTheDatabase.Id, Is.EqualTo(theNewIdReturned));
			Assert.That(theEntityReadFromTheDatabase.EntityName, Is.EqualTo(newEntity.EntityName));
		}

		[Test]
		public async Task Test_that_a_record_can_be_edited_on_the_collection()
		{
			// Arrange
			var newEntity = GetNewEntity();
			var theNewIdReturned = await TheSampleRepositoryWithIntId.AddOneAsync(newEntity);
			var theEntityReadFromTheDatabase = await TheSampleRepositoryWithIntId.GetOneByIdAsync(theNewIdReturned);

			// Act
			theEntityReadFromTheDatabase.EntityName = "Edited name";
			var theReplaceOneResult = await TheSampleRepositoryWithIntId.FindOneAndReplaceAsync(theEntityReadFromTheDatabase);
			var theUpdatedEntityReadFromTheDatabase = await TheSampleRepositoryWithIntId.GetOneByIdAsync(theNewIdReturned);

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
			var theNewIdReturned = await TheSampleRepositoryWithIntId.AddOneAsync(newEntity);
			var theEntityReadFromTheDatabase = await TheSampleRepositoryWithIntId.GetOneByIdAsync(theNewIdReturned);

			// Act
			var actualDeleteResult = await TheSampleRepositoryWithIntId.DeleteOneAsync(theEntityReadFromTheDatabase);
			var searchResultAfterDeletion = await TheSampleRepositoryWithIntId.GetOneByIdAsync(theNewIdReturned);

			// Assert
			Assert.That(actualDeleteResult.IsAcknowledged, Is.True);
			Assert.That(actualDeleteResult.DeletedCount, Is.EqualTo(1));
			Assert.That(searchResultAfterDeletion, Is.Null);		}

	    #region Private helper methods

	    private void Initialise()
	    {
	        DoInitialCleanUpOfCollections(TheTestCollectionName);

	        TheSampleRepositoryWithIntId = new SampleRepositoryWithIntId(DbContext, TheTestCollectionName);
	    }

	    private SampleEntityWithIntId GetNewEntity()
	    {
	        return new SampleEntityWithIntId()
	        {
	            Id = IdValueCounter++,                     // Cheap and nasty autoincrement for INT Id values - Should use an autoincrement key generator and assign it to the BSON serializer, etc..
	            EntityName = "New Entity with INT Id"
	        };
	    }

	    #endregion
	}
}