using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TaskProjectManagement.Api.DTOs.Auth;
using TaskProjectManagement.Api.Models;
using TaskProjectManagement.Api.DTOs.Projects;

namespace TaskProjectManagement.Api.Tests;

public class ProjectsControllerTests
{
    [Fact]
    public async Task CreateProject_WithoutAuthentication_ReturnsUnauthorized()
    {
        // Arrange
        await using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var dto = new CreateProjectDto
        {
            Name = "Test Project",
            Description = "Test project description"
        };

        // Act
        var response = await client.PostAsJsonAsync(
            "/api/Projects",
            dto);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProject_AsRegularUser_ReturnsForbidden()
    {
        // Arrange
        await using var factory = new CustomWebApplicationFactory();
        using var client = factory.CreateClient();

        var email = $"user{Guid.NewGuid()}@example.com";
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

        var loginResponse = await client.PostAsJsonAsync(
            "/api/Auth/login",
            loginDto);

        var loginResult =
            await loginResponse.Content.ReadFromJsonAsync<JsonElement>();

        var token = loginResult.GetProperty("token").GetString();

        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                token);

        // Act
        var response = await client.DeleteAsync(
            "/api/Projects/1");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
public async Task DeleteProject_AsAdmin_ReturnsNoContent()
{
    // Arrange
    await using var factory = new CustomWebApplicationFactory();
    using var client = factory.CreateClient();

    var adminEmail = $"admin{Guid.NewGuid()}@example.com";
    var adminPassword = "Admin@12345";

    // Create Admin user
    using (var scope = factory.Services.CreateScope())
    {
        var userManager =
            scope.ServiceProvider.GetRequiredService<
                UserManager<ApplicationUser>>();

        var admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Test",
            LastName = "Admin",
            EmailConfirmed = true
        };

        var createResult =
            await userManager.CreateAsync(
                admin,
                adminPassword);

        Assert.True(
            createResult.Succeeded,
            string.Join(
                ", ",
                createResult.Errors.Select(e => e.Description)));

        var roleResult =
            await userManager.AddToRoleAsync(
                admin,
                "Admin");

        Assert.True(
            roleResult.Succeeded,
            string.Join(
                ", ",
                roleResult.Errors.Select(e => e.Description)));
    }

    // Login as Admin
    var loginDto = new LoginDto
    {
        Email = adminEmail,
        Password = adminPassword
    };

    var loginResponse = await client.PostAsJsonAsync(
        "/api/Auth/login",
        loginDto);

    Assert.Equal(
        HttpStatusCode.OK,
        loginResponse.StatusCode);

    var loginResult =
        await loginResponse.Content.ReadFromJsonAsync<JsonElement>();

    var token =
        loginResult.GetProperty("token").GetString();

    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer",
            token);

    // Create a project first
    var createDto = new CreateProjectDto
    {
        Name = "Admin Delete Test",
        Description = "Project created for delete test"
    };

    var createResponse = await client.PostAsJsonAsync(
        "/api/Projects",
        createDto);

    Assert.Equal(
        HttpStatusCode.Created,
        createResponse.StatusCode);

    var createdProject =
        await createResponse.Content.ReadFromJsonAsync<JsonElement>();

    var projectId =
        createdProject.GetProperty("id").GetInt32();

    // Act
    var response = await client.DeleteAsync(
        $"/api/Projects/{projectId}");

    // Assert
    Assert.Equal(
        HttpStatusCode.NoContent,
        response.StatusCode);
}
  
    [Fact]
public async Task UpdateProject_AsOwner_ReturnsOk()
{
    // Arrange
    await using var factory = new CustomWebApplicationFactory();
    using var client = factory.CreateClient();

    var email = $"owner{Guid.NewGuid()}@example.com";
    var password = "Test@12345";

    var registerDto = new RegisterDto
    {
        FirstName = "Project",
        LastName = "Owner",
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

    var loginResponse = await client.PostAsJsonAsync(
        "/api/Auth/login",
        loginDto);

    var loginResult =
        await loginResponse.Content.ReadFromJsonAsync<JsonElement>();

    var token = loginResult.GetProperty("token").GetString();

    client.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer",
            token);

    // Create a project owned by this user
    var createDto = new CreateProjectDto
    {
        Name = "Owner Project",
        Description = "Project for update test"
    };

    var createResponse = await client.PostAsJsonAsync(
        "/api/Projects",
        createDto);

    Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

    var createdProject =
        await createResponse.Content.ReadFromJsonAsync<JsonElement>();

    var projectId = createdProject.GetProperty("id").GetInt32();

    // Update project
    var updateDto = new CreateProjectDto
    {
        Name = "Updated Project",
        Description = "Updated description"
    };

    // Act
    var response = await client.PutAsJsonAsync(
        $"/api/Projects/{projectId}",
        updateDto);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
}

[Fact]
public async Task GetProjects_DoesNotCauseNPlusOneQueries()
{
    // Arrange
    await using var factory = new CustomWebApplicationFactory();
    using var client = factory.CreateClient();

    var interceptor =
        factory.Services.GetRequiredService<QueryCountingInterceptor>();

    interceptor.Reset();

    // Act
    var response = await client.GetAsync(
        "/api/Projects?page=1&pageSize=10");

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    Assert.True(
        interceptor.QueryCount <= 2,
        $"Expected at most 2 SQL SELECT queries, but got {interceptor.QueryCount}.");
}
}