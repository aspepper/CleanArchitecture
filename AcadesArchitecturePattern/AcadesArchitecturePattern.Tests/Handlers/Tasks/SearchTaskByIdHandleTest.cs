using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Application.Handlers.Tasks;
using AcadesArchitecturePattern.Domain.Events;
using AcadesArchitecturePattern.Domain.Interfaces;
using AcadesArchitecturePattern.Domain.Queries.Tasks;
using AcadesArchitecturePattern.Shared.Enums;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Handlers.Tasks
{
    public class SearchTaskByIdHandleTest
    {
        [Fact]
        public async System.Threading.Tasks.Task SearchTaskByIdHandleWithValidIdShouldReturnSuccessResult()
        {
            // Arrange
            var query = new SearchTaskByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var taskServiceMock = new Mock<ITaskService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            var searchedTask = new Domain.Entities.ToDoTask("Test Task", "This is a test task", EnTaskPriorityLevel.High, EnStatusTask.ToDo, DateTime.Now, Guid.NewGuid());
            taskServiceMock.Setup(x => x.SearchById(query.Id)).Returns(searchedTask);

            var handle = new SearchTaskByIdHandle(taskServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefa encontrada!");
            result.Data.Should().BeEquivalentTo(searchedTask);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Once);
        }
    }
}
