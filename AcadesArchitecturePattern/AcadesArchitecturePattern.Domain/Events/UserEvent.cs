using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class UserEvent(User item) : BaseEvent
    {
        public User Item { get; } = item;
        public DateTime EventDateTime { get; } = DateTime.Now;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            UserEvent other = (UserEvent)obj;

            return Item.Id == other.Item.Id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
