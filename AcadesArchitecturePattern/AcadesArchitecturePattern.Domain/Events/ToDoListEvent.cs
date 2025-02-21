using AcadesArchitecturePattern.Domain.Entities;
using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class ToDoListEvent(ToDoList item) : BaseEvent
    {
        public ToDoList Item { get; } = item;
        public DateTime EventDateTime { get; } = DateTime.Now;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            ToDoListEvent other = (ToDoListEvent)obj;

            return Item.Id == other.Item.Id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
