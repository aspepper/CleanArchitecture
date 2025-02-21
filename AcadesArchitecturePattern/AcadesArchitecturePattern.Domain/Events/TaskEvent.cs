using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class TaskEvent(Entities.ToDoTask item) : BaseEvent
    {
        public Entities.ToDoTask Item { get; } = item;
        public DateTime EventDateTime { get; } = DateTime.Now;

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TaskEvent other = (TaskEvent)obj;

            return Item.Id == other.Item.Id;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}