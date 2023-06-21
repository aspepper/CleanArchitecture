using AdviceCompliance.Application.Common.Mappings;
using AdviceCompliance.Domain.Entities;

namespace AdviceCompliance.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
