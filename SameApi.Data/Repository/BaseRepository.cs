using Google.Cloud.Firestore;
using SameApi.Data.DbContexts;
using SameApi.Data.Model;

namespace SameApi.Data.Repository
{
    public abstract class BaseRepository<TContext, TModelDao> : IBaseRepository<TContext, TModelDao>
        where TModelDao : class, IModelDao
        where TContext : IBaseDbContext
    {
        public TContext _context { get; }
        protected abstract string CollectionName { get; }
        protected CollectionReference Collection => _context.Db.Collection(CollectionName);

        protected BaseRepository(TContext context) => _context = context;

        public virtual async Task<IEnumerable<TModelDao>> GetAllAsync()
        {
            var snap = await Collection.GetSnapshotAsync();
            return snap.Documents.Select(d => d.ConvertTo<TModelDao>()).ToList();
        }

        public virtual async Task<TModelDao?> GetByIdAsync(string id)
        {
            var snap = await Collection.Document(id).GetSnapshotAsync();
            return snap.Exists ? snap.ConvertTo<TModelDao>() : null;
        }

        public virtual async Task AddAsync(TModelDao entity)
        {
            var doc = await Collection.AddAsync(entity);
            entity.Id = doc.Id;
        }

        public virtual Task UpdateAsync(TModelDao entity) =>
            Collection.Document(entity.Id).SetAsync(entity, SetOptions.MergeAll);

        public virtual Task RemoveAsync(TModelDao entity) => RemoveByIdAsync(entity.Id);

        public virtual Task RemoveByIdAsync(string id) =>
            Collection.Document(id).DeleteAsync();
    }
}