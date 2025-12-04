using Xunit;
using FluentAssertions;

namespace snglrtycrvtureofspce.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        const int expected = 1;

        // Act
        const int actual = 1;

        // Assert
        actual.Should().Be(expected);
    }
}
