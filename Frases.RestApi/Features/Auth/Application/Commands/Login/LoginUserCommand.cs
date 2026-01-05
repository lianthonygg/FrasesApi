using FrasesApi.Shared.Application.Common.Abstractions;

namespace FrasesApi.Features.Auth.Application.Commands.Login;

public sealed class LoginUserCommand : ICommand<LoginUserResponseDto>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}