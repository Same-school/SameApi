using SameApi.Data.DbContexts;
using SameApi.Data.Model;

namespace SameApi.Data.Repository
{
    public interface IBaseRepository<TContext, TModelDao>
        where TModelDao : class, IModelDao
        where TContext : IBaseDbContext
    {
        TContext _context { get; }

        Task<IEnumerable<TModelDao>> GetAllAsync();
        Task<TModelDao?> GetByIdAsync(string id);
        Task AddAsync(TModelDao entity);
        Task UpdateAsync(TModelDao entity);
        Task RemoveAsync(TModelDao entity);
        Task RemoveByIdAsync(string id);
    }
}