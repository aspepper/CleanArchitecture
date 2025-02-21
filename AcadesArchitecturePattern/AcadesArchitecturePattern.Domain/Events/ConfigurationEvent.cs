using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class ConfigurationEvent(Configuration item) : BaseEvent
    {
        public Configuration Item { get; } = item;
        public DateTime EventDateTime { get; } = DateTime.Now;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ConfigurationEvent other = (ConfigurationEvent)obj;

            return Item.Id == other.Item.Id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
