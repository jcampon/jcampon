using MongoDB.Driver;

namespace JCampon.MongoDB.Repositories
{
    public interface IMongoDbDatabaseContext
    {
        // Just here for mocking on unit tests

        IMongoDatabase Database { get; }
    }
}