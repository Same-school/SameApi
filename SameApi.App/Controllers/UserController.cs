using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SameApi.Business.User.Command;
using SameApi.Business.User.Query;

namespace SameApi.App.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id, CancellationToken ct)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery { Id = id}, ct);
                return user is null ? NotFound() : Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { message = ex.Message });
            }
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var uid = User.FindFirst("user_id")?.Value;
            if (uid is null) return Unauthorized();

            var user = await _mediator.Send(new GetUserByIdQuery { Id = uid });
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost("use-profile")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command, CancellationToken ct)
        {
            var uid = User.FindFirst("user_id")?.Value;
            if (uid is null) return Unauthorized();

            command.Uid = uid;
            await _mediator.Send(command, ct);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateUserAsync( [FromBody] UpdateUserCommand command)
        {
            var uid = User.FindFirst("user_id")?.Value;
            if (uid is null) return Unauthorized();

            command.Id = uid;
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            await _mediator.Send(new DeleteUserCommand { Id = id});
            return Ok();
        }
    }
}