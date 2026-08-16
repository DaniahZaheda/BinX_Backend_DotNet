using IdentityDemo.Services;

namespace IdentityDemo.Tests;

public class TaskServiceTests
{
    [Fact]
    public void IsValidTaskTitle_ValidTitle_ReturnsTrue()
    {
        // Arrange
        var service = new TaskService();

        // Act
        var result = service.IsValidTaskTitle("Create API");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValidTaskTitle_ShortTitle_ReturnsFalse()
    {
        // Arrange
        var service = new TaskService();

        // Act
        var result = service.IsValidTaskTitle("Hi");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValidTaskTitle_EmptyTitle_ReturnsFalse()
    {
        // Arrange
        var service = new TaskService();

        // Act
        var result = service.IsValidTaskTitle("");

        // Assert
        Assert.False(result);
    }


    [Theory]
[InlineData("Create API", true)]
[InlineData("Hi", false)]
[InlineData("", false)]
public void IsValidTaskTitle_ReturnsExpectedResult(
    string title,
    bool expected)
{
    // Arrange
    var service = new TaskService();

    // Act
    var result = service.IsValidTaskTitle(title);

    // Assert
    Assert.Equal(expected, result);
}
}

