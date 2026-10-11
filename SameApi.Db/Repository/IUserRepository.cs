using SameApi.Data.Repository;
using SameApi.Db.DbContexts;
using SameApi.Model;

namespace SameApi.Db.Repository
{
    public interface IUserRepository : IBaseRepository<IApiSameDbContext, UserDao>
    {
        Task CreateAsync(UserDao user);                 // utilise user.Id (UID Firebase) comme ID du document
        Task<bool> EmailExistsAsync(string? username);
        Task UpdateFieldsAsync(string id, Dictionary<string, object> updates); 
    }
}