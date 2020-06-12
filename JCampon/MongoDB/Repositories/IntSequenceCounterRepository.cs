using JCampon.MongoDB.Entities;
using MongoDB.Driver;

namespace JCampon.MongoDB.Repositories
{
    public class IntSequenceCounterRepository : MongoDbRepositoryWithStringId<IntSequenceCounterEntity>
    {
        protected IntSequenceCounterEntity IntSequenceCounter;

        public IntSequenceCounterRepository(IMongoCollection<IntSequenceCounterEntity> collection) : base(collection)
        {
            IntSequenceCounter = new IntSequenceCounterEntity(collection.Database.DatabaseNamespace.DatabaseName, collection.CollectionNamespace.CollectionName);
        }

        /// <summary>
        /// The mechanism to get next number.
        /// </summary>
        /// <param name="key">
        /// The sequence key (should use the static fields on this class)
        /// </param>
        /// <returns>
        /// The get next number.
        /// </returns>
        public int GetNextNumber()
        {
            try
            {
                // for this key - increment the sequence number
                var sequenceQueryBuilder = Builders<IntSequenceCounterEntity>.Filter.Eq(secRecord => secRecord.Id, IntSequenceCounter.Id);
                var sequenceUpdateBuilder = Builders<IntSequenceCounterEntity>.Update.Inc(secRecord => secRecord.CurrentValueOnIntSequenceCounter, 1);
                var updateOptions = new FindOneAndUpdateOptions<IntSequenceCounterEntity>
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After
                };

                var sequence = Collection.FindOneAndUpdate(sequenceQueryBuilder, sequenceUpdateBuilder, updateOptions);

                #region Explanation of internal MongoDB behaviour for this code 

                /*
                   This .FindOneAndUpdate(..) method with the updateOptions passed should return the updated record with the
                   new value for the Seq field if there was already a record in there
                   Otherwise, if there was no initial record to begin with, then as we're doing an "upsert" here, what happens
                   is that a new record will be created and the Seq field will have the initial value of 1 as passed on the Inc() operation 
                   The Inc() would fail on an existing record with that field NULL, but this should never be the case as either the whole
                   record does not exists - and hence the upsert creates it - or it should be fully setup with a value on the Seq field
                 
                   To verify this behaviour I've run the following query in Mongo, first on an empty collection and then repeatedly 
                   and it works as expected. The first run would create the record and set the value of Seq to 1. Subsequent runs
                   would only do the update, incrementing the Seq by 1 every time
                 
                    db.getCollection('Test')
                        .update(
                            {_id: "IndexForSomething"},
                            {$inc: {"Seq": 1}}, 
                            {upsert: true}
                        )

                 */

                #endregion

                return sequence.CurrentValueOnIntSequenceCounter;
            }
            catch (MongoCommandException ex)
            {
                if (ex.Message.Contains("No matching object found"))
                {
                    this.CreateNewIntSequenceCounter();
                    return this.GetNextNumber();
                }
            }

            return -1;
        }


        #region PRIVATE HELPER FUNCTIONS

        /// <summary>
        /// The create new sequence.
        /// </summary>
        /// <param name="key">
        /// The sequence name.
        /// </param>
        private void CreateNewIntSequenceCounter()
        {
            Collection.InsertOne(IntSequenceCounter);
        }

        #endregion
    }
}