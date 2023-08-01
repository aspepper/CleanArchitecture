using AcadesArchitecturePattern.Domain.Commands.Users;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Commands.Users
{
    public class CreateUserCommandTest
    {
        [Fact]
        public void CreateUserWithValidDataShouldBeValid()
        {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "password123";

            var command = new CreateUserCommand(userName, email, password);

            // Act
            command.Validate();

            // Assert
            command.IsValid.Should().BeTrue();
            command.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void CreateUserWithInvalidDataShouldBeInvalid()
        {
            // Arrange
            var userName = "";
            var email = "invalidEmail";
            var password = "123";

            var command = new CreateUserCommand(userName, email, password);

            // Act
            command.Validate();

            // Assert
            command.IsValid.Should().BeFalse();
            command.Notifications.Should().NotBeEmpty();
        }
    }
}
