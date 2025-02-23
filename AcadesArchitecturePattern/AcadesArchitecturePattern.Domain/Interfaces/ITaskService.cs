namespace AcadesArchitecturePattern.Domain.Interfaces
{
    public interface ITaskService
    {
        // Commands:
        void Add(Domain.Entities.ToDoTask task);

        void Delete(Guid? id);
        void DeleteTasksByIdList(Guid? idList);

        void Update(Domain.Entities.ToDoTask task);

        // Queries:
        IEnumerable<Domain.Entities.ToDoTask> List();
        Domain.Entities.ToDoTask? SearchById(Guid? id);
    }
}
