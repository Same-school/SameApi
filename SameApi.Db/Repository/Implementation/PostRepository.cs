using Google.Cloud.Firestore;
using SameApi.Data.Repository;
using SameApi.Db.DbContexts;
using SameApi.Model;

namespace SameApi.Db.Repository.Implementation
{
    public class PostRepository(IApiSameDbContext context)
        : BaseRepository<IApiSameDbContext, PostDao>(context), IPostRepository
    {
        protected override string CollectionName => "posts";

        public async Task CreateAsync(PostDao post)
        {
            var db = Collection.Database;
            var postRef = Collection.Document();
            var userRef = db.Collection("users").Document(post.AuthorId);
            var now = Timestamp.GetCurrentTimestamp();

            post.CreatedAt = now;
            post.UpdatedAt = now;
            post.IsDeleted = false;

            await db.RunTransactionAsync(tx =>
            {
                tx.Create(postRef, post);
                tx.Update(userRef, new Dictionary<string, object>
                {
                    ["postsCount"] = FieldValue.Increment(1),
                    ["updatedAt"] = now
                });
                return Task.CompletedTask;
            });
            post.Id = postRef.Id;
        }
    }
}