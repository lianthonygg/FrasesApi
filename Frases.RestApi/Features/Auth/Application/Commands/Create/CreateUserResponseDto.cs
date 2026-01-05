using FrasesApi.Shared.Domain.Constants;

namespace FrasesApi.Features.Auth.Application.Commands.Create;

public sealed record CreateUserResponseDto(Guid Id, string CompanyName, string Token, Roles Rol);

        
    
