using AcadesArchitecturePattern.Domain.Commands.Users;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcadesArchitecturePattern.Api.Controllers
{
    [Route("v1/users")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        // Dependency Injection:

        private readonly IMediator mediator = mediator;

        // Commands:

        // Register a new user
        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserCommand command)
        {
            var result = await mediator.Send(command);

            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        // Delete a user
        [HttpDelete("delete/{id?}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await mediator.Send(command);

            return result.Success ? NoContent() : BadRequest(result.Message);
        }

        // Queries:

        // List all users
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var query = new ListUserQuery();
            var result = await mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        // Search user by email
        [HttpGet("searchEmail/{email}")]
        public async Task<IActionResult> SearchByEmail(string email)
        {
            var query = new SearchUserByEmailQuery { Email = email };
            var result = await mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        // Search user by id
        [HttpGet("searchId/{id?}")]
        public async Task<IActionResult> SearchById(Guid id)
        {
            var query = new SearchUserByIdQuery { Id = id };
            var result = await mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }
    }
}
