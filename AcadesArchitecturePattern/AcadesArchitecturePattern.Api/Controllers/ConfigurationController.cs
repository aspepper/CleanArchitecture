using AcadesArchitecturePattern.Api.Helper;
using AcadesArchitecturePattern.Domain.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AcadesArchitecturePattern.Api.Controllers
{
    [Route("v1/authentications")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        // Dependency Injection:

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfigurationController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }


        // Commands:


        // Get configuration settings
        [HttpGet("GetConfig/{Company}")]
        public async Task<IActionResult> GetConfig(string Company)
        {
            var query = new GetConfigurationByCompanyQuery { Company = Company };
            var result = await _mediator.Send(query);

            if (result.Success)
            {
                var config = UtilHelper.GetObjectItem(result, "Data");
                if (config != null)
                {
                    config = UtilHelper.GetObjectItem(config, "Item");
                    if (config != null)
                    {
                        var t = config.GetType();
                        var session = _httpContextAccessor.HttpContext.Session;
                        foreach (var prop in t.GetProperties())
                        {
                            if (prop.PropertyType.Name == "String")
                            {
                                var value = prop.GetValue(config);
                                //AppContext.SetData("configuration", item);
                                //Environment.SetEnvironmentVariable(prop.Name, value?.ToString());
                                session.SetString(prop.Name, value != null ? value.ToString() : "");
                            }
                        }
                    }
                }
            }

            return result.Success ? Ok(result.Data) : BadRequest(result.Message);
        }

    }
}
