using AcadesArchitecturePattern.Domain.Entities;

namespace AcadesArchitecturePattern.Domain.Interfaces
{
    public interface IUserService
    {
        // Commands:
        void Add(User user);

        void Delete(Guid? id);

        // Queries:
        IEnumerable<User> List();
        User? SearchById(Guid? id);
        User? SearchByEmail(string email);
        User? SearchByUserName(string userName);
    }
}
