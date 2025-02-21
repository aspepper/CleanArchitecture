using AcadesArchitecturePattern.Application.Services;
using AcadesArchitecturePattern.Domain.Commands.Authentications;
using AcadesArchitecturePattern.Domain.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcadesArchitecturePattern.Api.Controllers
{
    [Route("v1/authentications")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // Dependency Injection:

        private readonly IMediator mediator;
        private readonly JwtTokenGenerator tokenGenerator;
        private readonly AuthenticateTokenMappingService authenticateTokenMappingService;

        public LoginController(IMediator mediator, JwtTokenGenerator tokenGenerator, AuthenticateTokenMappingService authenticateTokenMappingService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.tokenGenerator = tokenGenerator;
            this.authenticateTokenMappingService = authenticateTokenMappingService;

            var session = httpContextAccessor.HttpContext?.Session;
            if (session?.GetString("AdvLinkWebApi") == null)
            {
                ConfigurationController config = new(mediator, httpContextAccessor);
                var apiConfigSystem = configuration["APIConfigSystem"];
                _ = config.GetConfig(apiConfigSystem == null ? "" : apiConfigSystem.ToString());
            }
        }

        // Commands:

        // Login pelo Usuário e Senha
        [HttpPost("signIn")]
        public async Task<IActionResult> SignInUserName(LoginUserNameCommand command)
        {
            var result = await mediator.Send(command);

            if (result.Success)
            {
                var authenticateToken = authenticateTokenMappingService.MapAuthenticateTokenFromResult(result.Data);
                var token = tokenGenerator.GenerateToken(authenticateToken);

                return Ok(new { user = authenticateToken, token });
            }

            return BadRequest(result.Message);
        }
    }
}
