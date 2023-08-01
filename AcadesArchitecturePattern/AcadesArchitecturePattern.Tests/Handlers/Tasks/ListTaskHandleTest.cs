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
    public class ListTaskHandleTest
    {
        [Fact]
        public async void ListTaskHandleWithTasksShouldReturnSuccessResult()
        {
            // Arrange
            var query = new ListTaskQuery();
            var taskServiceMock = new Mock<ITaskService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            var tasks = new List<Domain.Entities.ToDoTask>
            {
                new Domain.Entities.ToDoTask("Task 1", "Description 1", EnTaskPriorityLevel.High, EnStatusTask.ToDo, null, Guid.NewGuid()),
                new Domain.Entities.ToDoTask("Task 2", "Description 2", EnTaskPriorityLevel.Medium, EnStatusTask.ToDo, DateTime.Now, Guid.NewGuid()),
                new Domain.Entities.ToDoTask("Task 3", "Description 3", EnTaskPriorityLevel.Low, EnStatusTask.Done, DateTime.Now, Guid.NewGuid())
            };

            taskServiceMock.Setup(x => x.List()).Returns(tasks);

            var handle = new ListTaskHandle(taskServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefas encontradas!");
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<List<Domain.Entities.ToDoTask>>();
            var taskList = (List<Domain.Entities.ToDoTask>)result.Data;
            taskList.Count.Should().Be(tasks.Count);
            taskList.Select(t => t.Name).Should().BeEquivalentTo(tasks.Select(t => t.Name));
            taskServiceMock.Verify(x => x.List(), Times.Once);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Exactly(tasks.Count));
        }

        [Fact]
        public async void ListTaskHandleWithNoTasksShouldReturnErrorResult()
        {
            // Arrange
            var query = new ListTaskQuery();
            var taskServiceMock = new Mock<ITaskService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            taskServiceMock.Setup(x => x.List()).Returns(new List<Domain.Entities.ToDoTask>());

            var handle = new ListTaskHandle(taskServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Tarefas não encontradas");
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<List<Domain.Entities.ToDoTask>>();
            var taskList = (List<Domain.Entities.ToDoTask>)result.Data;
            taskList.Should().BeEmpty();
            taskServiceMock.Verify(x => x.List(), Times.Once);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Never);
        }
    }
}
