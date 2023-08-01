using AcadesArchitecturePattern.Domain.Commands.Tasks;
using AcadesArchitecturePattern.Shared.Enums;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Commands.Tasks
{
    public class UpdateTaskCommandTest
    {
        [Fact]
        public void UpdateTaskWithValidDataShouldBeValid()
        {
            // Arrange
            var command = new UpdateTaskCommand
            {
                Name = "Updated Task",
                Description = "This is an updated task",
                Priority = EnTaskPriorityLevel.Medium,
                Status = EnStatusTask.ToDo,
                Reminder = null
            };

            // Act
            command.Validate();

            // Assert
            command.IsValid.Should().BeTrue();
            command.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void UpdateTaskWithNullDataShouldBeValid()
        {
            // Arrange
            var command = new UpdateTaskCommand
            {
                Name = null,
                Description = null,
                Priority = null,
                Status = null,
                Reminder = null
            };

            // Act
            command.Validate();

            // Assert
            command.IsValid.Should().BeTrue();
            command.Notifications.Should().BeEmpty();
        }
    }
}
