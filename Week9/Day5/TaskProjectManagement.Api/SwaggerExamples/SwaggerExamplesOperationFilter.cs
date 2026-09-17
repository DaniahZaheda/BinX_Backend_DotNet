using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskProjectManagement.Api.SwaggerExamples;

public class SwaggerExamplesOperationFilter : IOperationFilter
{
    public void Apply(
        OpenApiOperation operation,
        OperationFilterContext context)
    {
        var path = context.ApiDescription.RelativePath;
        var method = context.ApiDescription.HttpMethod;

        // ============================
        // Register
        // ============================

        if (method == "POST" &&
            path?.Equals(
                "api/Auth/register",
                StringComparison.OrdinalIgnoreCase) == true)
        {
            if (operation.RequestBody?.Content.ContainsKey("application/json") == true)
            {
                operation.RequestBody.Content["application/json"].Example =
                    new OpenApiObject
                    {
                        ["firstName"] =
                            new OpenApiString("Dania"),

                        ["lastName"] =
                            new OpenApiString("Zaheda"),

                        ["email"] =
                            new OpenApiString("dania@example.com"),

                        ["password"] =
                            new OpenApiString("P@ssword123")
                    };
            }

            if (operation.Responses.ContainsKey("200"))
            {
                operation.Responses["200"].Content["application/json"] =
                    new OpenApiMediaType
                    {
                        Example = new OpenApiObject
                        {
                            ["message"] =
                                new OpenApiString(
                                    "User registered successfully."),

                            ["userId"] =
                                new OpenApiString(
                                    "8f7c2c8e-1234-4567-8901-123456789abc"),

                            ["email"] =
                                new OpenApiString(
                                    "dania@example.com")
                        }
                    };
            }
        }

        // ============================
        // Login
        // ============================

        if (method == "POST" &&
            path?.Equals(
                "api/Auth/login",
                StringComparison.OrdinalIgnoreCase) == true)
        {
            if (operation.RequestBody?.Content.ContainsKey("application/json") == true)
            {
                operation.RequestBody.Content["application/json"].Example =
                    new OpenApiObject
                    {
                        ["email"] =
                            new OpenApiString("dania@example.com"),

                        ["password"] =
                            new OpenApiString("P@ssword123")
                    };
            }

            if (operation.Responses.ContainsKey("200"))
            {
                operation.Responses["200"].Content["application/json"] =
                    new OpenApiMediaType
                    {
                        Example = new OpenApiObject
                        {
                            ["token"] =
                                new OpenApiString(
                                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."),

                            ["expiresAt"] =
                                new OpenApiString(
                                    "2026-09-15T12:00:00Z"),

                            ["userId"] =
                                new OpenApiString(
                                    "8f7c2c8e-1234-4567-8901-123456789abc"),

                            ["email"] =
                                new OpenApiString(
                                    "dania@example.com"),

                            ["roles"] =
                                new OpenApiArray
                                {
                                    new OpenApiString("User")
                                }
                        }
                    };
            }
        }

        // ============================
        // Create Project
        // ============================

        if (method == "POST" &&
            path?.Equals(
                "api/Projects",
                StringComparison.OrdinalIgnoreCase) == true)
        {
            if (operation.RequestBody?.Content.ContainsKey("application/json") == true)
            {
                operation.RequestBody.Content["application/json"].Example =
                    new OpenApiObject
                    {
                        ["name"] =
                            new OpenApiString(
                                "Travel Management System"),

                        ["description"] =
                            new OpenApiString(
                                "A project for managing travel activities and tasks.")
                    };
            }

            if (operation.Responses.ContainsKey("201"))
            {
                operation.Responses["201"].Content["application/json"] =
                    new OpenApiMediaType
                    {
                        Example = new OpenApiObject
                        {
                            ["id"] =
                                new OpenApiInteger(1),

                            ["name"] =
                                new OpenApiString(
                                    "Travel Management System"),

                            ["description"] =
                                new OpenApiString(
                                    "A project for managing travel activities and tasks."),

                            ["createdAt"] =
                                new OpenApiString(
                                    "2026-09-14T12:00:00Z")
                        }
                    };
            }
        }
    }
}