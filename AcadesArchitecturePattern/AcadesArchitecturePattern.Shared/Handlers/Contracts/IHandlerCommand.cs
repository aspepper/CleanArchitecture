using AcadesArchitecturePattern.Shared.Commands;

namespace AcadesArchitecturePattern.Shared.Handlers.Contracts
{
    public interface IHandlerCommand<T> where T : ICommand
    {
        ICommandResult Handler(T command);
    }
}
