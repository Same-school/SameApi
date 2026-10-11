using Google.Cloud.Firestore;

namespace SameApi.Data.DbContexts
{
    public interface IBaseDbContext
    {
        FirestoreDb Db { get; }
    }
}
