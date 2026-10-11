using AutoMapper;
using Google.Cloud.Firestore;
using MediatR;
using SameApi.Db.UnitOfWork;
using SameApi.Dto;
using SameApi.Model;

namespace SameApi.Business.User.Command
{
    public class CreateUserCommand : UserInput, IRequest
    {
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IApiSameUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            IApiSameUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.UserRepository.EmailExistsAsync(request.Email))
                throw new InvalidOperationException(
                    "Un utilisateur avec cet e-mail existe déjà.");

            var now = Timestamp.GetCurrentTimestamp();

            UserDao user = _mapper.Map<UserDao>(request);

            if (request.Email != null )
                user.Username = request.Email.Split('@')[0];

            user.BirthDate = request.BirthDate?.ToString("yyyy-MM-dd");

            user.CreatedAt = now;
            user.UpdatedAt = now;

            await _uow.UserRepository.CreateAsync(user);
        }
    }
}