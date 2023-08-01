using AcadesArchitecturePattern.Application.Handlers.Events;
using AcadesArchitecturePattern.Application.Handlers.Tasks;
using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Domain.Entities;
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
    public class CreateTaskHandleTest
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateTaskHandleWithValidDataShouldReturnSuccessResult()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Name = "Test Task",
                Description = "This is a test task",
                Priority = EnTaskPriorityLevel.High,
                Status = EnStatusTask.ToDo,
                Reminder = DateTime.Now,
                IdList = Guid.NewGuid()
            };

            var taskServiceMock = new Mock<ITaskService>();
            var listServiceMock = new Mock<IToDoListService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            listServiceMock.Setup(x => x.SearchById(command.IdList)).Returns(new ToDoList("Test List", EnColor.Red, command.IdList));

            var handle = new CreateTaskHandle(taskServiceMock.Object, listServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Tarefa criada com sucesso!");
            result.Data.Should().NotBeNull();
            taskServiceMock.Verify(x => x.Add(It.IsAny<Domain.Entities.ToDoTask>()), Times.Once);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTaskHandleWithInvalidDataShouldReturnErrorResult()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Name = "",
                Description = "This is a test task",
                Priority = EnTaskPriorityLevel.High,
                Status = EnStatusTask.ToDo,
                Reminder = null,
                IdList = Guid.NewGuid()
            };

            var taskServiceMock = new Mock<ITaskService>();
            var listServiceMock = new Mock<IToDoListService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            var handle = new CreateTaskHandle(taskServiceMock.Object, listServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Insira corretamente os dados da tarefa");
            result.Data.Should().BeNull();
            taskServiceMock.Verify(x => x.Add(It.IsAny<Domain.Entities.ToDoTask>()), Times.Never);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTaskHandleWithNonexistentListShouldReturnErrorResult()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Name = "Test Task",
                Description = "This is a test task",
                Priority = EnTaskPriorityLevel.High,
                Status = EnStatusTask.ToDo,
                Reminder = DateTime.Now,
                IdList = Guid.NewGuid()
            };

            var taskServiceMock = new Mock<ITaskService>();
            var listServiceMock = new Mock<IToDoListService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            listServiceMock.Setup(x => x.SearchById(command.IdList)).Returns((ToDoList)null);

            var handle = new CreateTaskHandle(taskServiceMock.Object, listServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Lista não encontrada");
            result.Data.Should().BeNull();
            taskServiceMock.Verify(x => x.Add(It.IsAny<Domain.Entities.ToDoTask>()), Times.Never);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Never);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateTaskHandleWithInvalidTaskShouldReturnErrorResult()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Name = "Test Task",
                Description = "This is a test task",
                Priority = EnTaskPriorityLevel.High,
                Status = EnStatusTask.ToDo,
                Reminder = DateTime.Now,
                IdList = Guid.NewGuid()
            };

            var taskServiceMock = new Mock<ITaskService>();
            var listServiceMock = new Mock<IToDoListService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            listServiceMock.Setup(x => x.SearchById(command.IdList)).Returns(new ToDoList("Test List", EnColor.Red, command.IdList));

            var invalidTask = new Domain.Entities.ToDoTask("", "", EnTaskPriorityLevel.Low, EnStatusTask.ToDo, null, Guid.Empty);
            taskServiceMock.Setup(x => x.Add(It.IsAny<Domain.Entities.ToDoTask>())).Callback((Domain.Entities.ToDoTask task) => invalidTask = task);

            var handle = new CreateTaskHandle(taskServiceMock.Object, listServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Dados de tarefa inválidos");
            result.Data.Should().BeEquivalentTo(invalidTask);
            taskServiceMock.Verify(x => x.Add(It.IsAny<Domain.Entities.ToDoTask>()), Times.Once);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Never);
        }


        [Fact]
        public async System.Threading.Tasks.Task CreateTaskHandleWithExceptionShouldReturnErrorResult()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Name = "Test Task",
                Description = "This is a test task",
                Priority = EnTaskPriorityLevel.High,
                Status = EnStatusTask.ToDo,
                Reminder = DateTime.Now,
                IdList = Guid.NewGuid()
            };

            var taskServiceMock = new Mock<ITaskService>();
            var listServiceMock = new Mock<IToDoListService>();
            var loggerMock = new Mock<ILogger<TaskEventHandle>>();
            var mediatorMock = new Mock<IMediator>();

            listServiceMock.Setup(x => x.SearchById(command.IdList)).Throws(new Exception("Some error message"));

            var handle = new CreateTaskHandle(taskServiceMock.Object, listServiceMock.Object, loggerMock.Object, mediatorMock.Object);

            // Act
            var result = await handle.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Ocorreu um erro ao criar a tarefa");
            result.Data.Should().BeNull();
            taskServiceMock.Verify(x => x.Add(It.IsAny<Domain.Entities.ToDoTask>()), Times.Never);
            mediatorMock.Verify(x => x.Publish(It.IsAny<TaskEvent>(), CancellationToken.None), Times.Never);
        }
    }
}
