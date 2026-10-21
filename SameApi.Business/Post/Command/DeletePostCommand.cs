using AutoMapper;
using Google.Cloud.Firestore;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using SameApi.Db.UnitOfWork;
using SameApi.Dto.Post;
using SameApi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SameApi.Business.Post.Command
{
    public class DeletePostCommand : IRequest
    {
        public required string PostId { get; set; }
    }
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IApiSameUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeletePostCommandHandler(
            IApiSameUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _uow.PostRepository.GetByIdAsync(request.PostId) ?? throw new NullReferenceException("Erro when checking PostId, This id doesn't exist");

            post.IsDeleted = true;
            post.UpdatedAt = Timestamp.GetCurrentTimestamp();

            await _uow.PostRepository.UpdateAsync(post);
        }
    }
}
