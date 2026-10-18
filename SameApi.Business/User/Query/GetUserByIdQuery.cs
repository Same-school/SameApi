using AutoMapper;
using MediatR;
using SameApi.Db.UnitOfWork;
using SameApi.Dto.User;

namespace SameApi.Business.User.Query
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public string Id { get; set; } = default!;
        public string? RequesterId { get; set; }

    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse?>
    {
        readonly IApiSameUnitOfWork _uow;
        readonly IMapper _mapper;

        public GetUserByIdQueryHandler(
            IApiSameUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _uow.UserRepository.GetByIdAsync(request.Id);

            if (data == null)
                return null;

            if (data.IsPrivate == true && data.Id != request.RequesterId)
                throw new UnauthorizedAccessException("Ce compte est privé.");

            var result = _mapper.Map<UserResponse>(data);
            return result;
        }
    }
}