using AcadesArchitecturePattern.Domain.Queries.Users;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Queries.Users
{
    public class SearchUserByEmailQueryTest
    {
        [Fact]
        public void SearchUserByEmailQueryShouldBeValid()
        {
            // Arrange
            var query = new SearchUserByEmailQuery
            {
                Email = "test@example.com"
            };

            // Act
            query.Validate();

            // Assert
            query.IsValid.Should().BeTrue();
            query.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void SearchUserByEmailQueryWithEmptyEmailShouldBeInvalid()
        {
            // Arrange
            var query = new SearchUserByEmailQuery
            {
                Email = ""
            };

            // Act
            query.Validate();

            // Assert
            query.IsValid.Should().BeFalse();
            query.Notifications.Should().NotBeEmpty();
        }

        [Fact]
        public void SearchUserByEmailQueryWithInvalidEmailShouldBeInvalid()
        {
            // Arrange
            var query = new SearchUserByEmailQuery
            {
                Email = "invalidEmail"
            };

            // Act
            query.Validate();

            // Assert
            query.IsValid.Should().BeFalse();
            query.Notifications.Should().NotBeEmpty();
        }
    }
}
