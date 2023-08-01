using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Application.Handlers.Tasks;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Commands;
using AcadesArchitecturePattern.Shared.Enums;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Handlers.Tasks
{
    public class DeleteTaskHandleTest
    {
        [Fact]
        public async Task DeleteTaskHandleWithValidDataShouldReturnSuccessResult()
        {
            // Arrange
            var command = new DeleteTaskCommand
            {
                Id = Guid.NewGuid()
            };

            var taskServiceMock = new Mock<ITaskService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            var searchedTask = new Domain.Entities.ToDoTask("Test Task", "This is a test task", EnTaskPriorityLevel.High, EnStatusTask.ToDo, DateTime.Now, Guid.NewGuid());
            taskServiceMock.Setup(x => x.SearchById(command.Id)).Returns(searchedTask);

            var handle = new DeleteTaskHandle(taskServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefa excluída com sucesso!");
            result.Data.Should().Be("");
            taskServiceMock.Verify(x => x.Delete(searchedTask.Id), Times.Once);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Once);
        }
    }
}
