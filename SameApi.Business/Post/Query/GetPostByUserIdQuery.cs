using AutoMapper;
using MediatR;
using SameApi.Db.UnitOfWork;
using SameApi.Dto.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace SameApi.Business.Post.Query
{
    public class GetPostByUserIdQuery : IRequest<List<PostResponse?>>
    {
        public required string UserId { get; set; }
    }
    public class GetPostByUserIdQueryHandler : IRequestHandler<GetPostByUserIdQuery, List<PostResponse?>>
    {
        readonly IApiSameUnitOfWork _uow;
        readonly IMapper _mapper;

        public GetPostByUserIdQueryHandler(IApiSameUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<PostResponse?>> Handle(GetPostByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _uow.PostRepository.GetByUserIdAsync(request.UserId);
            result = result.Where(x => x.IsDeleted == false).ToList();

            return _mapper.Map<List<PostResponse?>>(result);
        }
    }
}
