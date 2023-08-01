using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class UserEvent : BaseEvent
    {
        public UserEvent(User item)
        {
            Item = item;
            EventDateTime = DateTime.Now;
        }

        public User Item { get; }
        public DateTime EventDateTime { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            UserEvent other = (UserEvent)obj;

            return Item.Id == other.Item.Id;
        }
    }
}
