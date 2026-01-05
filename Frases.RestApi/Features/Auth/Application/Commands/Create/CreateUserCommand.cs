using FrasesApi.Shared.Application.Common.Abstractions;
using FrasesApi.Shared.Domain.Constants;

namespace FrasesApi.Features.Auth.Application.Commands.Create;

public sealed class CreateUserCommand : ICommand<CreateUserResponseDto>
{
    public required string FullName { get; init; }            
    public required string Nickname { get; init; }               
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required Roles Rol { get; init; }
    public string? Phone { get; init; } 
}
