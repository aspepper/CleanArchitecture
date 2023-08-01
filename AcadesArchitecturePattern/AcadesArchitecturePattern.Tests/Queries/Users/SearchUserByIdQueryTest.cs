using AcadesArchitecturePattern.Domain.Queries.Users;
using FluentAssertions;
using MediatR;
using System;
using Xunit;

namespace AcadesArchitecturePattern.Tests.Queries.Users
{
    public class SearchUserByIdQueryTest
    {
        [Fact]
        public void SearchUserByIdQueryShouldBeValid()
        {
            // Arrange
            var query = new SearchUserByIdQuery
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
