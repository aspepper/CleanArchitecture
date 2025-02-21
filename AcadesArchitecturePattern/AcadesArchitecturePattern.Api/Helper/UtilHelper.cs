using MediatR;

namespace AcadesArchitecturePattern.Api.Helper
{
    public class UtilHelper(IMediator mediator, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {

        private readonly IMediator mediator = mediator;
        private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
        private readonly IConfiguration configuration = configuration;

        public static object? GetObjectItem(object obj, string item)
        {
            Type t = obj.GetType();
            foreach (var prop in t.GetProperties())
            {
                if (prop.Name.Equals(item, StringComparison.CurrentCultureIgnoreCase))
                {
                    return prop.GetValue(obj);
                }
            }
            return null;
        }

    }
}
