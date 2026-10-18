using AutoMapper;
using Google.Cloud.Firestore;
using MediatR;
using SameApi.Db.UnitOfWork;
using SameApi.Dto.Comment;
using SameApi.Model;
using System.Text.Json.Serialization;

namespace SameApi.Business.Comment.Command
{
    public class CreateCommentCommand : CommentInput, IRequest
    {
        [JsonIgnore]
        public string? Uid { get; set; } = default!;
        public required string PostId { get; set; }
        public string? ParentCommentId { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly IApiSameUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateCommentCommandHandler(
            IApiSameUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var now = Timestamp.GetCurrentTimestamp();
            var comment = _mapper.Map<CommentDao>(request);

            comment.AuthorId = request.Uid!;
            comment.PostId = request.PostId;
            comment.ParentCommentId = request.ParentCommentId;
            comment.CreatedAt = now;

            await _uow.CommentRepository.CreateAsync(comment);
        }
    }
}