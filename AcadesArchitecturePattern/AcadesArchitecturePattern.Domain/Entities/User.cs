using AcadesArchitecturePattern.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;

namespace AcadesArchitecturePattern.Domain.Entities
{
    public class User : Base
    {
        public User(string userName, string email, string password)
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(userName, "userName", "O campo 'userName' não pode estar vazio")
                .IsEmail(email, "email", "Digite um endereço de e-mail válido")
                .IsGreaterThan(password, 6, "O campo 'senha' deve ter no mínimo 6 caracteres")
            );

            if (IsValid)
            {
                UserName = userName;
                Email = email;
                Password = password;
            }
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Compositions
        public IReadOnlyCollection<ToDoList> ToDoLists { get; private set; }
        private List<ToDoList> _lists { get; set; }
    }
}
