using SameApi.Db.DbContexts;
using SameApi.Db.Repository;

namespace SameApi.Db.UnitOfWork
{
    public class SameApiUnitOfWork : IApiSameUnitOfWork
    {
        public IApiSameDbContext Context  { get; }
        public IUserRepository UserRepository { get; }

        public SameApiUnitOfWork(
            IApiSameDbContext context,
            IUserRepository repository)
        {
            Context = context;
            UserRepository = repository;

        }

        //public async Task<int> SaveChangesAsync()
        //{
        //    return await Context.Db.();
        //}

    }
}