using Google.Cloud.Firestore;

namespace SameApi.Db.DbContexts
{
    public class SameApiDbContext(FirestoreDb db) : IApiSameDbContext
    {
        public FirestoreDb Db { get; } = db;
    }
}