using AutoMapper;
using MediatR;
using SameApi.Db.UnitOfWork;
using SameApi.Dto.Post;

namespace SameApi.Business.Post.Query
{
    public class GetPostByIdQuery : IRequest<PostResponse?>
    {
        public required string Id { get; set; }
    }
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostResponse?>
    {
        readonly IApiSameUnitOfWork _uow;
        readonly IMapper _mapper;

        public GetPostByIdQueryHandler(IApiSameUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PostResponse?> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.PostRepository.GetByIdAsync(request.Id);
            
            return  _mapper.Map<PostResponse>(result);
        }
    }
}