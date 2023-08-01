using AcadesArchitecturePattern.Domain.Commands.Users;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Commands.Users
{
    public class DeleteUserCommandTest
    {
        [Fact]
        public void DeleteUserWithValidIdShouldBeValid()
        {
            // Arrange
            var id = Guid.NewGuid();

            var command = new DeleteUserCommand
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
        public void DeleteUserWithEmptyIdShouldBeInvalid()
        {
            // Arrange
            var id = Guid.Empty;

            var command = new DeleteUserCommand
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
