using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class ConfigurationEvent : BaseEvent
    {
        public ConfigurationEvent(Configuration item)
        {
            Item = item;
            EventDateTime = DateTime.Now;
        }

        public Configuration Item { get; }
        public DateTime EventDateTime { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ConfigurationEvent other = (ConfigurationEvent)obj;

            return Item.Id == other.Item.Id;
        }
    }
}
