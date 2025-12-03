using Xunit;
using FluentAssertions;

namespace snglrtycrvtureofspce.Core.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var expected = 1;
        
        // Act
        var actual = 1;
        
        // Assert
        actual.Should().Be(expected);
    }
}
