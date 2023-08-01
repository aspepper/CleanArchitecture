using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Shared.Enums;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Commands.Tasks
{
    public class CreateTaskCommandTest
    {
        [Fact]
        public void CreateTaskWithValidDataShouldBeValid()
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

            // Act
            command.Validate();

            // Assert
            command.IsValid.Should().BeTrue();
            command.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void CreateTaskWithInvalidDataShouldBeInvalid()
        {
            // Arrange
            var name = "";
            var description = "This is a test task";
            var priority = EnTaskPriorityLevel.High;
            var status = EnStatusTask.ToDo;
            DateTime? reminder = null;
            var idList = Guid.NewGuid();  // Usando Guid.NewGuid() para gerar um novo valor de Guid

            var command = new CreateTaskCommand
            {
                Name = name,
                Description = description,
                Priority = priority,
                Status = status,
                Reminder = reminder,
                IdList = idList
            };

            // Act
            command.Validate();

            // Assert
            command.IsValid.Should().BeFalse();
            command.Notifications.Should().NotBeEmpty();
        }
    }
}
