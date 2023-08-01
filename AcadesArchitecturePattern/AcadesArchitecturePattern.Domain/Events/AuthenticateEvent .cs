using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class AuthenticateEvent : BaseEvent
    {
        public AuthenticateEvent(AuthenticateResponse item)
        {
            Item = item;
            EventDateTime = DateTime.Now;
        }

        public AuthenticateResponse Item { get; }
        public DateTime EventDateTime { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            AuthenticateEvent other = (AuthenticateEvent)obj;

            return Item.Id == other.Item.Id;
        }
    }
}
