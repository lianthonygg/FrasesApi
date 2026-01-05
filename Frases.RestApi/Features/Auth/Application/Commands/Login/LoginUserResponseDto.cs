using FrasesApi.Shared.Domain.Constants;

namespace FrasesApi.Features.Auth.Application.Commands.Login;

public sealed record LoginUserResponseDto(
    string AccessToken, Guid UserId, Roles Rol
);