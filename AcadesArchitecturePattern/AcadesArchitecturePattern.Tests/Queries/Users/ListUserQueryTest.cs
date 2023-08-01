using AcadesArchitecturePattern.Domain.Queries.Users;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Queries.Users
{
    public class ListUserQueryTest
    {
        [Fact]
        public void ListUserQueryShouldBeValid()
        {
            // Arrange
            var query = new ListUserQuery();

            // Act
            query.Validate();

            // Assert
            query.IsValid.Should().BeTrue();
            query.Notifications.Should().BeEmpty();
        }
    }
}
