using AcadesArchitecturePattern.Domain.Commands.Authentications;
using AcadesArchitecturePattern.Domain.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcadesArchitecturePattern.Api.Controllers
{
    [Route("v1/authentications")]
    [ApiController]
    public class LoginController(IMediator mediator) : ControllerBase
    {
        // Dependency Injection:

        private readonly IMediator mediator = mediator;
        // Commands:

        // Logon by UserName and Password
        [HttpPost("signIn")]
        public async Task<IActionResult> SignInUserName(LoginUserNameCommand command)
        {
            var query = new SearchUserByUserNameQuery { UserName = command.UserName, Password = command.Password };
            var result = await mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

        // Logon by Email and Password
        [HttpPost("signInEmail")]
        public async Task<IActionResult> SignInEmail(LoginEmailCommand command)
        {
            var query = new SearchUserByEmailQuery { Email = command.Email, Password = command.Password };
            var result = await mediator.Send(query);

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }
    }
}