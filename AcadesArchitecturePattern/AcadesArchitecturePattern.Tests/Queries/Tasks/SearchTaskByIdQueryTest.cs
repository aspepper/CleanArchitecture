using AcadesArchitecturePattern.Domain.Queries.Tasks;
using FluentAssertions;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Queries.Tasks
{
    public class SearchTaskByIdQueryTest
    {
        [Fact]
        public void SearchTaskByIdQueryShouldBeValid()
        {
            // Arrange
            var query = new SearchTaskByIdQuery
            {
                Id = Guid.NewGuid()
            };

            // Act
            query.Validate();

            // Assert
            query.IsValid.Should().BeTrue();
            query.Notifications.Should().BeEmpty();
        }
    }
}
