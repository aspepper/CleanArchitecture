using AcadesArchitecturePattern.Domain.Queries.Tasks;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Queries.Tasks
{
    public class ListTaskQueryTest
    {
        [Fact]
        public void ListTaskQueryShouldBeValid()
        {
            // Arrange
            var query = new ListTaskQuery();

            // Act
            query.Validate();

            // Assert
            query.IsValid.Should().BeTrue();
            query.Notifications.Should().BeEmpty();
        }
    }
}
