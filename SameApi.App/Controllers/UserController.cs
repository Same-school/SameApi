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
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery{Id = id});
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost("use-profile")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id,  [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
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