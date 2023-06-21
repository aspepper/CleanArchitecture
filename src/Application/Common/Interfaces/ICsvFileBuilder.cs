using AdviceCompliance.Application.TodoLists.Queries.ExportTodos;

namespace AdviceCompliance.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
