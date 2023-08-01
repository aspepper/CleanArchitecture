using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Application.Handlers.Tasks;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Shared.Enums;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Handlers.Tasks
{
    public class UpdateTaskHandleTest
    {
        [Fact]
        public async System.Threading.Tasks.Task UpdateTaskHandleWithValidDataShouldReturnSuccessResult()
        {
            // Arrange
            var command = new UpdateTaskCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Task",
                Description = "This is an updated task",
                Priority = EnTaskPriorityLevel.Medium,
                Status = EnStatusTask.ToDo,
                Reminder = DateTime.Now.AddDays(1)
            };

            var taskServiceMock = new Mock<ITaskService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            var oldTask = new Domain.Entities.ToDoTask("Old Task", "This is the old task", EnTaskPriorityLevel.Low, EnStatusTask.ToDo, null, Guid.NewGuid());
            taskServiceMock.Setup(x => x.SearchById(command.Id)).Returns(oldTask);

            var handle = new UpdateTaskHandle(taskServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefa alterada com sucesso!");
            result.Data.Should().NotBeNull();
            taskServiceMock.Verify(x => x.Update(It.IsAny<Domain.Entities.ToDoTask>()), Times.Once);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Once);
        }
    }
}
