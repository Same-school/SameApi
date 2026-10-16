using SameApi.Data.Repository;
using SameApi.Db.DbContexts;
using SameApi.Model;

namespace SameApi.Db.Repository.Implementation
{
    public class UserRepository(IApiSameDbContext context)
        : BaseRepository<IApiSameDbContext, UserDao>(context), IUserRepository
    {
        protected override string CollectionName => "users";

        public async Task CreateAsync(UserDao user)
        {
            var document = Collection.Document(user.Id);
            await document.CreateAsync(user);
        }

        public async Task<bool> EmailExistsAsync(string? email)
        {
            var snap = await Collection.WhereEqualTo("email", email).Limit(1).GetSnapshotAsync();
            return snap.Count > 0;
        }

        public async Task UpdateFieldsAsync(string id, Dictionary<string, object> updates)
        {
            var document = Collection.Document(id);
            await document.UpdateAsync(updates);
        }
    }
}