using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Shared.Entities;
using AcadesArchitecturePattern.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;

namespace AcadesArchitecturePattern.Domain.Entities
{
    public class ToDoTask : Base
    {
        public ToDoTask()
        {
            Name = string.Empty;
            Description = string.Empty;
            Priority = EnTaskPriorityLevel.None;
            Status = EnStatusTask.Done;
            Reminder = null;
            IdList = new Guid();
        }

        public ToDoTask(string name, string description, EnTaskPriorityLevel priority, EnStatusTask status, DateTime? reminder, Guid idList)
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(name, "Name", "The 'Name' field cannot be empty")
                    .IsNotEmpty(description, "Description", "O campo 'Descrição' não pode estar vazio")
                    .IsNotNull(priority, "Priority", "O campo 'Prioridade' não pode ser nulo")
                    .IsNotNull(status, "Status", "O campo 'Status' não pode ser nulo")
                    .IsNotNull(reminder, "Reminder", "O campo 'Lembrete' não pode ser nulo")
                    .IsNotEmpty(idList, "IdList", "O campo 'IdList' não pode estar vazio")
            );

            Name = name;
            Description = description;
            Priority = priority;
            Status = status;
            Reminder = reminder;
            IdList = idList;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public EnTaskPriorityLevel Priority { get; set; }
        public EnStatusTask Status { get; set; }
        public DateTime? Reminder { get; set; }

        // FK's
        public Guid IdList { get; set; }
        public ToDoList Lists { get; set; } = new ToDoList("Default Title", EnColor.White, new Guid()); // Initialize Lists property with required parameters

        // Configs:
        public void UpdateTask(string? name = null, string? description = null, EnTaskPriorityLevel? priority = null, EnStatusTask? status = null, DateTime? reminder = null)
        {
            if (!IsValid)
                return;

            // Sets the value of properties if property is not null; otherwise, keeps the current value of the property.
            Name = name ?? Name;
            Description = description ?? Description;
            Priority = priority ?? Priority;
            Status = status ?? Status;
            Reminder = reminder ?? Reminder;

            ModifyDate = DateTime.Now;
        }

        // Events
        private bool done;
        private readonly List<TaskEvent> domainEvents = [];

        public bool Done
        {
            get => done;
            set
            {
                if (value && !done)
                {
                    AddDomainEvent(new TaskEvent(this));
                }

                done = value;
            }
        }

        // Add event in list
        private void AddDomainEvent(TaskEvent @event)
        {
            domainEvents.Add(@event);
        }

        // Get task's event
        public IReadOnlyList<TaskEvent> GetDomainEvents()
        {
            return domainEvents.AsReadOnly();
        }

        // Mark task as done
        public void MarkAsDone()
        {
            if (!done && Status == EnStatusTask.Done)
            {
                done = true;
                AddDomainEvent(new TaskEvent(this));
            }
        }
    }
}
