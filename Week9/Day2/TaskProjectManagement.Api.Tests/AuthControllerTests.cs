using System.Net;
using System.Text.Json;
using System.Net.Http.Json;
using TaskProjectManagement.Api.DTOs.Auth;

namespace TaskProjectManagement.Api.Tests;

public class AuthControllerTests
{
    [Fact]
    public async Task Register_WithValidData_ReturnsOk()
    {
        // Arrange
        await using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new RegisterDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = $"test{Guid.NewGuid()}@example.com",
            Password = "Test@12345"
        };

        // Act
        var response = await client.PostAsJsonAsync(
            "/api/Auth/register",
            dto);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
public async Task Register_WithDuplicateEmail_ReturnsBadRequest()
{
    // Arrange
    await using var factory = new CustomWebApplicationFactory();
    using var client = factory.CreateClient();

    var email = $"duplicate{Guid.NewGuid()}@example.com";

    var dto = new RegisterDto
    {
        FirstName = "Test",
        LastName = "User",
        Email = email,
        Password = "Test@12345"
    };

    // Act
    var firstResponse = await client.PostAsJsonAsync(
        "/api/Auth/register",
        dto);

    var secondResponse = await client.PostAsJsonAsync(
        "/api/Auth/register",
        dto);

    // Assert
    Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);
    Assert.Equal(HttpStatusCode.BadRequest, secondResponse.StatusCode);
}

[Fact]
public async Task Login_WithValidCredentials_ReturnsOk()
{
    // Arrange
    await using var factory = new CustomWebApplicationFactory();
    using var client = factory.CreateClient();

    var email = $"login{Guid.NewGuid()}@example.com";
    var password = "Test@12345";

    var registerDto = new RegisterDto
    {
        FirstName = "Test",
        LastName = "User",
        Email = email,
        Password = password
    };

    await client.PostAsJsonAsync(
        "/api/Auth/register",
        registerDto);

    var loginDto = new LoginDto
    {
        Email = email,
        Password = password
    };

    // Act
    var response = await client.PostAsJsonAsync(
        "/api/Auth/login",
        loginDto);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    var result = await response.Content.ReadFromJsonAsync<JsonElement>();

    Assert.True(result.TryGetProperty("token", out var token));
    Assert.False(string.IsNullOrWhiteSpace(token.GetString()));
}
[Fact]
public async Task Login_WithInvalidPassword_ReturnsUnauthorized()
{
    // Arrange
    await using var factory = new CustomWebApplicationFactory();
    using var client = factory.CreateClient();

    var email = $"invalid{Guid.NewGuid()}@example.com";
    var correctPassword = "Test@12345";

    var registerDto = new RegisterDto
    {
        FirstName = "Test",
        LastName = "User",
        Email = email,
        Password = correctPassword
    };

    await client.PostAsJsonAsync(
        "/api/Auth/register",
        registerDto);

    var loginDto = new LoginDto
    {
        Email = email,
        Password = "WrongPassword@123"
    };

    // Act
    var response = await client.PostAsJsonAsync(
        "/api/Auth/login",
        loginDto);

    // Assert
    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
}
}
