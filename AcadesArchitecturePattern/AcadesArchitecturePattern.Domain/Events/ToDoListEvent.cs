using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class ToDoListEvent : BaseEvent
    {
        public ToDoListEvent(ToDoList item)
        {
            Item = item;
            EventDateTime = DateTime.Now;
        }

        public ToDoList Item { get; }
        public DateTime EventDateTime { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ToDoListEvent other = (ToDoListEvent)obj;

            return Item.Id == other.Item.Id;
        }
    }
}
