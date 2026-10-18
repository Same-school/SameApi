using SameApi.Data.Repository;
using SameApi.Db.DbContexts;
using SameApi.Model;

namespace SameApi.Db.Repository
{
    public interface IPostRepository : IBaseRepository<IApiSameDbContext, PostDao>
    {
        Task CreateAsync(PostDao post);
    }
}