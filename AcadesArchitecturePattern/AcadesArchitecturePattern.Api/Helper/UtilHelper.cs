using AcadesArchitecturePattern.Application.Services;
using AcadesArchitecturePattern.Domain.Queries.Users;
using AcadesArchitecturePattern.Domain.Security;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AcadesArchitecturePattern.Api.Helper
{
    public class UtilHelper
    {

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UtilHelper(IMediator mediator, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public static object? GetObjectItem(object obj, string item)
        {
            Type t = obj.GetType();
            foreach (var prop in t.GetProperties())
            {
                if (prop.Name.ToLower() == item.ToLower())
                {
                    return prop.GetValue(obj);
                }
            }
            return null;
        }

    }
}
