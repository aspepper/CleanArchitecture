using AcadesArchitecturePattern.Domain.Entities;

namespace AcadesArchitecturePattern.Domain.Interfaces
{
    public interface IToDoListService
    {
        // Commands:
        void Add(ToDoList toDo);
        void DeleteListsByIdUser(Guid? idUser);
        void Delete(Guid? id);

        // Queries:
        IEnumerable<ToDoList> List();
        ToDoList? SearchById(Guid? id);
    }
}
