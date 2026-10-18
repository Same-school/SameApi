using AutoMapper;
using Google.Cloud.Firestore;
using MediatR;
using SameApi.Db.UnitOfWork;
using SameApi.Dto.Post;
using SameApi.Model;
using System.Text.Json.Serialization;

namespace SameApi.Business.Post.Command
{
    public class CreatePostCommand : PostInput, IRequest
    {
        [JsonIgnore]
        public string? Uid { get; set; } = default!;
    }
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IApiSameUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreatePostCommandHandler(
            IApiSameUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var now = Timestamp.GetCurrentTimestamp();
            var post = _mapper.Map<PostDao>(request);
            post.AuthorId = request.Uid!;
            post.CreatedAt = now;

            await _uow.PostRepository.CreateAsync(post);
        }
    }
}