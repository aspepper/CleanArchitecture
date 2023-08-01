using AcadesArchitecturePattern.Domain.Entities;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Entities
{
    public class UserTest
    {
        [Fact]
        public void CreateUserWithValidDataShouldBeValid()
        {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "password123";

            // Act
            var user = new User(userName, email, password);

            // Assert
            Assert.True(user.IsValid);
        }

        [Fact]
        public void CreateUserWithEmptyUserNameShouldBeInvalid()
        {
            // Arrange
            var userName = "";
            var email = "test@example.com";
            var password = "password123";

            // Act
            var user = new User(userName, email, password);

            // Assert
            Assert.False(user.IsValid);
        }

        [Fact]
        public void CreateUserWithInvalidEmailShouldBeInvalid()
        {
            // Arrange
            var userName = "testUser";
            var email = "invalidEmail";
            var password = "password123";

            // Act
            var user = new User(userName, email, password);

            // Assert
            Assert.False(user.IsValid);
        }

        [Fact]
        public void CreateUserWithShortPasswordShouldBeInvalid()
        {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "123";

            // Act
            var user = new User(userName, email, password);

            // Assert
            Assert.False(user.IsValid);
        }

        [Fact]
        public void CreateUserWithValidDataShouldSetProperties()
        {
            // Arrange
            var userName = "testUser";
            var email = "test@example.com";
            var password = "password123";

            // Act
            var user = new User(userName, email, password);

            // Assert
            Assert.Equal(userName, user.UserName);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
        }

        [Fact]
        public void CreateUserWithInvalidDataShouldNotSetProperties()
        {
            // Arrange
            var userName = "";
            var email = "invalidEmail";
            var password = "123";

            // Act
            var user = new User(userName, email, password);

            // Assert
            Assert.NotEqual(userName, user.UserName);
            Assert.NotEqual(email, user.Email);
            Assert.NotEqual(password, user.Password);
        }
    }
}
