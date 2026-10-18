using MediatR;
using SameApi.Db.UnitOfWork;
using System.Text.Json.Serialization;

namespace SameApi.Business.User.Command
{
    public class UnfollowUserCommand : IRequest
    {
        [JsonIgnore]
        public string CurrentUserId { get; set; } = "";
        public string FollowUserId { get; set; } = "";
    }
    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand>
    {
        private readonly IApiSameUnitOfWork _uow;

        public UnfollowUserCommandHandler(IApiSameUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public async Task Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
        {
            if (request.CurrentUserId == request.FollowUserId)
                throw new InvalidOperationException("INVALID_FOLLOW: Impossible to follow himself.");

            _ = await _uow.UserRepository.GetByIdAsync(request.CurrentUserId)
                ?? throw new InvalidOperationException("USER_NOT_FOUND: Actual User not found.");
            _ = await _uow.UserRepository.GetByIdAsync(request.FollowUserId)
                ?? throw new InvalidOperationException("USER_NOT_FOUND: Follow User not found.");

            await _uow.UserRepository.UnfollowAsync(request.CurrentUserId, request.FollowUserId);
        }
    }
}