using AcadesArchitecturePattern.Shared.Events;

namespace AcadesArchitecturePattern.Domain.Events
{
    public class TaskEvent : BaseEvent
    {
        public TaskEvent(Entities.ToDoTask item)
        {
            Item = item;
            EventDateTime = DateTime.Now;
        }

        public Entities.ToDoTask Item { get; }
        public DateTime EventDateTime { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TaskEvent other = (TaskEvent)obj;

            return Item.Id == other.Item.Id;
        }

    }
}