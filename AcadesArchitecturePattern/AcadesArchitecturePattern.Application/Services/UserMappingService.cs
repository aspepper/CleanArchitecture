using AcadesArchitecturePattern.Domain.Entities;

namespace AcadesArchitecturePattern.Application.Services;

public class UserMappingService
{
    public User MapUserFromResult(object data)
    {
        if (data is User user)
        {
            return user;
        }

        throw new NotSupportedException("Tipo de dados inválido para o usuário.");
    }
}
