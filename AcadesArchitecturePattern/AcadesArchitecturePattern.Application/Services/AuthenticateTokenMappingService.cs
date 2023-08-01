using AcadesArchitecturePattern.Domain.Entities;

namespace AcadesArchitecturePattern.Application.Services
{
    public class AuthenticateTokenMappingService
    {
        public AuthenticateResponse MapAuthenticateTokenFromResult(object data)
        {
            if (data is AuthenticateResponse authenticateToken)
            {
                return authenticateToken;
            }

            throw new NotSupportedException("Tipo de dados inválido para o Token.");
        }
    }
}
