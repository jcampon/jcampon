using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCampon.MongoDB.Entities;
using JCampon.MongoDB.Repositories;
using JCampon.MongoDB.Repositories.Tools;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NUnit.Framework;

namespace JCampon.MongoDB.Tests
{
    [TestFixture]
    public class BaseTestsForMongoDbRepositories
	{
	    private const string DefaultTestCollectionForIntSequences = "TestIntSequences";
        private const string SettingsKeyForMongoDbConnectionString = "MongoDBConnectionString";

	    protected const string DefaultTestDatabaseName = "JCamponTestDatabase";
        protected readonly MongoClient Client;

	    public BaseTestsForMongoDbRepositories()
	    {
	        var connectionString = ConfigurationManager.AppSettings[SettingsKeyForMongoDbConnectionString];
            Client = new MongoClient(connectionString);

	        if (Client == null)
	            throw new MongoClientException("A new MongoDB client instance could not be created from the connection string settings provided");
        }

        #region Protected methods

	    [OneTimeSetUp]
        public void InitializeBaseTest()
	    {
	        ConfigureBsonClassMaps();
        }

	    private void ConfigureBsonClassMaps()
	    {
	        var listOfClassMapsRegistered = BsonClassMap.GetRegisteredClassMaps();

	        listOfClassMapsRegistered = BsonClassMap.GetRegisteredClassMaps();

	        var testIntSequenceCollection = Client.GetDatabase(DefaultTestDatabaseName).GetCollection<IntSequenceCounterEntity>(DefaultTestCollectionForIntSequences);
	        var intSequenceCounterRepository = new IntSequenceCounterRepository(testIntSequenceCollection);
	        var intSequenceCounterGenerator = new IntSequenceCounterGenerator(intSequenceCounterRepository);

	        BsonSerializer.RegisterIdGenerator(typeof(int), intSequenceCounterGenerator);

            var classMap = BsonClassMap.LookupClassMap(typeof(SampleEntityWithIntId));
            
            /*
	        if (!BsonClassMap.IsClassMapRegistered(typeof(SampleEntityWithIntId)))
	        {
	            BsonClassMap.RegisterClassMap<SampleEntityWithIntId>(cm =>
	            {
	                cm.AutoMap();
	                cm.SetIsRootClass(true);
	                cm.SetIgnoreExtraElements(true);
	                cm.SetIdMember(cm.GetMemberMap(c => c.Id));
	                cm.MapProperty(p => p.Id).SetIdGenerator(intSequenceCounterGenerator);
	            });
	        }
            */
        }

	    #endregion
    }
}
