using AcadesArchitecturePattern.Domain.Commands.Tasks;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Commands.Tasks
{
    public class DeleteTaskCommandTest
    {
        [Fact]
        public void DeleteTaskWithValidIdShouldBeValid()
        {
            // Arrange
            var id = Guid.NewGuid();

            var command = new DeleteTaskCommand
            {
                Id = id
            };

            // Act
            command.Validate();

            // Assert 
            command.IsValid.Should().BeTrue();
            command.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void DeleteTaskWithEmptyIdShouldBeInvalid()
        {
            // Arrange
            var id = Guid.Empty;

            var command = new DeleteTaskCommand
            {
                Id = id
            };

            // Act
            command.Validate();

            // Assert
            command.IsValid.Should().BeFalse();
            command.Notifications.Should().NotBeEmpty();
        }
    }
}
