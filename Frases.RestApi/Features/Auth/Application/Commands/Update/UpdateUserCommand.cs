using FrasesApi.Shared.Application.Common.Abstractions;

namespace FrasesApi.Features.Auth.Application.Commands.Update;

public sealed class UpdateUserCommand : ICommand<UpdateUserResponseDto>
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string NickName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
}