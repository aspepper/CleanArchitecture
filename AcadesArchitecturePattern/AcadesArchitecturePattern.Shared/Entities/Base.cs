using AcadesArchitecturePattern.Shared.Events;
using Flunt.Notifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcadesArchitecturePattern.Shared.Entities
{
    public abstract class Base : Notifiable<Notification>
    {
        protected Base()
        {
            Id = Guid.NewGuid();
            InsertDate = DateTime.Now;
            InsertedBy = null;
            ModifyDate = null;
            ModifiedBy = null;
        }

        public Guid Id { get; set; }
        public DateTime InsertDate { get; set; }
        public string? InsertedBy { get; set; } = string.Empty;
        public DateTime? ModifyDate { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty;


        // Events

        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
