using IdentityDemo.Models;
using IdentityDemo.Repositories;
using IdentityDemo.Services;
using Moq;

namespace IdentityDemo.Tests;

public class TaskServiceTests
{
    // Day 1 - Fact Tests

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

    // Day 1 - Theory Test

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

    // Day 2 - Moq: Return Value

    [Fact]
    public async Task GetTaskTitleAsync_TaskExists_ReturnsTitle()
    {
        // Arrange
        var mockRepository = new Mock<ITaskRepository>();

        mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(new TaskItem
            {
                Id = 1,
                Title = "Create API"
            });

        var service = new TaskManagementService(mockRepository.Object);

        // Act
        var result = await service.GetTaskTitleAsync(1);

        // Assert
        Assert.Equal("Create API", result);
    }

    // Day 2 - Moq: Exception

    [Fact]
    public async Task GetTaskTitleAsync_RepositoryThrowsException_ThrowsException()
    {
        // Arrange
        var mockRepository = new Mock<ITaskRepository>();

        mockRepository
            .Setup(r => r.GetByIdAsync(1))
            .ThrowsAsync(new InvalidOperationException());

        var service = new TaskManagementService(mockRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.GetTaskTitleAsync(1));
    }

    // Day 2 - Moq: Verify

    [Fact]
    public async Task UpdateTaskAsync_CallsRepositoryOnce()
    {
        // Arrange
        var mockRepository = new Mock<ITaskRepository>();

        var service = new TaskManagementService(mockRepository.Object);

        var task = new TaskItem
        {
            Id = 1,
            Title = "Updated Task"
        };

        // Act
        await service.UpdateTaskAsync(task);

        // Assert
        mockRepository.Verify(
            r => r.UpdateAsync(task),
            Times.Once);
    }
}