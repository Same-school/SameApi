using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameApi.Business.Post.Command;
using SameApi.Business.Post.Query;

namespace SameApi.App.Controllers
{
    [ApiController]
    [Route("api/post")]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostCommand command, CancellationToken ct)
        {
            var uid = User.FindFirst("user_id")?.Value;
            if (uid is null) return Unauthorized();

            command.Uid = uid;
            await _mediator.Send(command, ct);
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken ct)
        {
            var post = await _mediator.Send(new GetPostByIdQuery { Id = id }, ct);
            return post is null ? NotFound() : Ok(post);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId, CancellationToken ct)
        {
            var post = await _mediator.Send(new GetPostByUserIdQuery { UserId = userId }, ct);
            return post is null ? NotFound() : Ok(post);
        }
    }
}