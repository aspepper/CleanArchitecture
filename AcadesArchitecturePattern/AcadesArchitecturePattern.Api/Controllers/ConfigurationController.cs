using AcadesArchitecturePattern.Api.Helper;
using AcadesArchitecturePattern.Domain.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcadesArchitecturePattern.Api.Controllers
{
    [Route("v1/authentications")]
    [ApiController]
    public class ConfigurationController(IMediator mediator, IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        // Dependency Injection:

        private readonly IMediator mediator = mediator;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;


        // Commands:


        // Get configuration settings
        [HttpGet("GetConfig/{Company}")]
        public async Task<IActionResult> GetConfig(string Company)
        {
            var query = new GetConfigurationByCompanyQuery { Company = Company };
            var result = await mediator.Send(query);

            if (result.Success)
            {
                var config = UtilHelper.GetObjectItem(result, "Data");
                if (config != null)
                {
                    config = UtilHelper.GetObjectItem(config, "Item");
                    if (config != null)
                    {
                        var t = config.GetType();
                        var session = httpContextAccessor.HttpContext!.Session;
                        foreach (var prop in t.GetProperties())
                        {
                            if (prop.PropertyType.Name == "String")
                            {
                                var value = prop.GetValue(config);
                                //AppContext.SetData("configuration", item);
                                //Environment.SetEnvironmentVariable(prop.Name, value?.ToString());
                                session.SetString(prop.Name, value?.ToString() ?? string.Empty);
                            }
                        }
                    }
                }
            }

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

    }
}
