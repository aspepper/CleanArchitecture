using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Domain.Commands.Users;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AcadesArchitecturePattern.Application.Handlers.Users
{
    public class DeleteUserHandle : IRequestHandler<DeleteUserCommand, GenericCommandResult>
    {
        private readonly IUserService _userService;
        private readonly IToDoListService _listService;
        private readonly ILogger<ToDoListEventHandle> _logger;
        private readonly IMediator _mediator;

        public DeleteUserHandle(IUserService userService, ILogger<ToDoListEventHandle> logger, IMediator mediator, IToDoListService listService)
        {
            _userService = userService;
            _logger = logger;
            _mediator = mediator;
            _listService = listService;
        }

        public async Task<GenericCommandResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.Validate();

                if (!command.IsValid)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Informe corretamente o usuário que deseja excluir", command.Notifications));
                }

                var searchedUser = _userService.SearchById(command.Id);

                if (searchedUser == null)
                {
                    _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                    return await Task.FromResult(new GenericCommandResult(false, "Usuário não encontrado", command.Notifications));
                }

                _listService.DeleteListsByIdUser(searchedUser.Id);

                _userService.Delete(searchedUser.Id);

                _logger.LogInformation("Tarefa Concluída: {CommandName}", command.GetType().Name);

                var userEvent = new UserEvent(searchedUser);
                await _mediator.Publish(userEvent, cancellationToken);

                return await Task.FromResult(new GenericCommandResult(true, "Usuário excluído com sucesso!", ""));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Tarefa Falhou: {CommandName}", command.GetType().Name);
                return await Task.FromResult(new GenericCommandResult(false, "Ocorreu um erro ao excluir o usuário", ex.Message));
            }
        }
    }
}
