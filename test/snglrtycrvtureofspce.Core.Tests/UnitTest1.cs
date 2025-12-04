using Xunit;
using FluentAssertions;

namespace snglrtycrvtureofspce.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        const int Expected = 1;

        // Act
        const int Actual = 1;

        // Assert
        Actual.Should().Be(Expected);
    }
}
