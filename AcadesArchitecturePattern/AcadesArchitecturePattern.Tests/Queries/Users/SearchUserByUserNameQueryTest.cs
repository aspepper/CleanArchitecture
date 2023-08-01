using AcadesArchitecturePattern.Domain.Queries.Users;
using FluentAssertions;
using MediatR;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Queries.Users
{
    public class SearchUserByUserNameQueryTest
    {
        [Fact]
        public void SearchUserByUserNameQueryShouldBeValid()
        {
            // Arrange
            var query = new SearchUserByUserNameQuery
            {
                UserName = "testUser"
            };

            // Act
            query.Validate();

            // Assert
            query.IsValid.Should().BeTrue();
            query.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void SearchUserByUserNameQueryWithEmptyUserNameShouldBeInvalid()
        {
            // Arrange
            var query = new SearchUserByUserNameQuery
            {
                UserName = ""
            };

            // Act
            query.Validate();

            // Assert
            query.IsValid.Should().BeFalse();
            query.Notifications.Should().NotBeEmpty();
        }
    }
}
