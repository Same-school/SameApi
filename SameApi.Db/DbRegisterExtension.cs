using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using SameApi.Db.DbContexts;
using SameApi.Db.Repository;
using SameApi.Db.Repository.Implementation;
using SameApi.Db.UnitOfWork;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace SameApi.Db
{
    public static class DbRegisterExtension
    {
        public static void RegisterSameApiDbContainer(this IServiceCollection services)
        {

            services.AddScoped<IApiSameDbContext, SameApiDbContext>();
 
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IApiSameUnitOfWork, SameApiUnitOfWork>();
        }
        public static void RegisterFireStore(this IServiceCollection services, string projectId, string databaseId)
        {
            services.AddSingleton(_ => new FirestoreDbBuilder
            {
                ProjectId = projectId,
                DatabaseId = databaseId
            }.Build());
        }
    }
}