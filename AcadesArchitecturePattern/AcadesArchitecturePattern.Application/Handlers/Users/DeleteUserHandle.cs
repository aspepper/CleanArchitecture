using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Users;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Users
{
    public class DeleteUserHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator, IToDoListService listService) : IRequestHandler<DeleteUserCommand, GenericCommandResult>
    {
        private readonly IUserService userService = userService;
        private readonly IToDoListService listService = listService;
        private readonly ILogger<ToDoListEventHandle> logger = logger;
        private readonly IMediator mediator = mediator;

        public async Task<GenericCommandResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Informe corretamente o usuário que deseja excluir", command.Notifications));
                }

                var searchedUser = userService.SearchById(command.Id);

                if (searchedUser == null)
                {
                    logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Usuário não encontrado", command.Notifications));
                }

                listService.DeleteListsByIdUser(searchedUser.Id);

                userService.Delete(searchedUser.Id);

                logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var userEvent = new UserEvent(searchedUser);
                await mediator.Publish(userEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Usuário excluído com sucesso!", ""));
            }
            catch (Exception ex)
            {
                logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao excluir o usuário", ex.Message));
            }
        }
    }
}
