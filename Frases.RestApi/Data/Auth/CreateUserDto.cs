using FrasesApi.Features.Auth.Application.Commands.Create;
using FrasesApi.Shared.Domain.Constants;

namespace FrasesApi.Data.Auth;

public record CreateUserDto(
    string FullName,
    string Nickname,
    string Email,
    string Password,
    string? Phone,
    Roles Rol = Roles.User
);

public static class CreateUserDtoToCommand
{
    public static CreateUserCommand MapToCommand(this CreateUserDto dto)
    {
        return new CreateUserCommand
        {
            FullName = dto.FullName,
            Nickname = dto.Nickname,
            Email = dto.Email,
            Password = dto.Password,
            Rol = dto.Rol,
            Phone = dto.Phone
        };
    }
}