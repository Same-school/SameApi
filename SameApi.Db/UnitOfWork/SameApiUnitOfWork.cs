using SameApi.Db.DbContexts;
using SameApi.Db.Repository;

namespace SameApi.Db.UnitOfWork
{
    public class SameApiUnitOfWork : IApiSameUnitOfWork
    {
        public IApiSameDbContext Context  { get; }
        public IUserRepository UserRepository { get; }

        public IPostRepository PostRepository { get; }

        public ICommentRepository CommentRepository { get; }

        public SameApiUnitOfWork(
            IApiSameDbContext context,
            IUserRepository userRepository,
            IPostRepository postRepository,
            ICommentRepository commentRepository)
        {
            Context = context;
            UserRepository = userRepository;
            PostRepository = postRepository;
            CommentRepository = commentRepository;

        }

    }
}