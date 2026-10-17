using MediatR;
using SameApi.Db.UnitOfWork;
using System.Text.Json.Serialization;

namespace SameApi.Business.User.Command
{
    public class FollowUserCommand : IRequest
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "";
        public string FollowUserId { get; set; } = "";
    }
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand>
    {
        private readonly IApiSameUnitOfWork _uow;

        public FollowUserCommandHandler(IApiSameUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public async Task Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            if (request.CurrentUserId == request.FollowUserId)
                throw new InvalidOperationException("INVALID_FOLLOW: Impossible de se suivre soi-même.");

            _ = await _uow.UserRepository.GetByIdAsync(request.CurrentUserId)
                ?? throw new InvalidOperationException("USER_NOT_FOUND: Actual User not found.");
            _ = await _uow.UserRepository.GetByIdAsync(request.FollowUserId)
                ?? throw new InvalidOperationException("USER_NOT_FOUND: Follow User not found.");

            await _uow.UserRepository.FollowAsync(request.CurrentUserId, request.FollowUserId);
        }
    }
}