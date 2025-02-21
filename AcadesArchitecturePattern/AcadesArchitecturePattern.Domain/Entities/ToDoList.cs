using AcadesArchitecturePattern.Shared.Entities;
using AcadesArchitecturePattern.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;

namespace AcadesArchitecturePattern.Domain.Entities
{
    public class ToDoList : Base
    {
        public ToDoList(string title, EnColor color, Guid idUser)
        {
            AddNotifications(
            new Contract<Notification>()
            .Requires()
                .IsNotEmpty(title, "Title", "O campo 'Título' não pode estar vazio")
                .IsNotNull(color, "Color", "O campo 'Cor' não pode ser nulo")
                .IsNotEmpty(idUser, "IdUser", "O campo 'IdUser' não pode estar vazio")
                );

            Title = title;
            Color = color;
            IdUser = idUser;
            Users = new User("defaultUserName", "defaultEmail", "defaultPassword"); // Initialize Users property with default values
            taskList = []; // Initialize taskList property
            Tasks = taskList.AsReadOnly(); // Initialize Tasks property
        }

        public string Title { get; set; }
        public EnColor Color { get; set; } = EnColor.White;

        // FK's
        public Guid IdUser { get; set; }
        public User Users { get; set; }

        // Compositions
        public IReadOnlyCollection<ToDoTask> Tasks { get; private set; }
        private List<ToDoTask> taskList { get; set; }
    }
}
