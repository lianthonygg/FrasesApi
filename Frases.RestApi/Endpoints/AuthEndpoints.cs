using System.Security.Claims;
using FrasesApi.Data.Auth;
using FrasesApi.Features.Auth.Application.Commands.Create;
using FrasesApi.Features.Auth.Application.Commands.Login;
using FrasesApi.Features.Auth.Application.Commands.Update;
using FrasesApi.Shared.Application.Common.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApplication1.Data.Employer;

namespace FrasesApi.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Authentication")
            .WithOpenApi();

        authGroup.MapPost("/login", async (LoginUserDto request, [FromServices] ICommandHandler<LoginUserCommand, LoginUserResponseDto> handler) =>
        {
            var command = request.MapToCommand();
            var result = await handler.Handle(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Login a user",
            Description = "Login a user through its credentials",
            Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Authentication" } }
        });

        authGroup.MapGet("/me",
                (ClaimsPrincipal user, HttpContext context) =>
                {
                    Log.Information($"🔍 /auth/me called");
                    Log.Information(
                        $"🔍 Headers: {string.Join(", ", context.Request.Headers.Select(h => $"{h.Key}={h.Value}"))}");
                    Log.Information($"🔍 User: {user?.Identity?.Name}");
                    Log.Information(
                        $"🔍 Claims: {string.Join(", ", user?.Claims.Select(c => $"{c.Type}={c.Value}") ?? [])}");

                    var email = user?.FindFirst(ClaimTypes.Email)?.Value;
                    var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var role = user?.FindFirst(ClaimTypes.Role)?.Value;


                    return Results.Ok(new
                    {
                        email,
                        userId,
                        role,
                        fullName = user?.FindFirst("fullName")?.Value,
                        nickname = user?.FindFirst("nickname")?.Value,
                        phone = user?.FindFirst("phone")?.Value,
                        accessToken = user?.FindFirst("accessToken")?.Value
                    });

                })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Get current user",
                Description = "Get current logged user data",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Authentication" } }
            })
            .RequireAuthorization();

        authGroup.MapPost("/user", async (CreateUserDto request, [FromServices] ICommandHandler<CreateUserCommand, CreateUserResponseDto> handler) =>
            {
                var command = request.MapToCommand();
                var result = await handler.Handle(command);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Creates a user",
                Description = "Creates a user in the system",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Authentication" } }
            });

        authGroup.MapPut("/user/{id}", async (Guid id, UpdateUserDto request, [FromServices] ICommandHandler<UpdateUserCommand, UpdateUserResponseDto> handler) =>
            {
                var command = request.MapToCommand(id);
                var result = await handler.Handle(command);
                
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Updates an user",
                Description = "Updates an existing user in the system",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Authentication" } }
            });
    }
}