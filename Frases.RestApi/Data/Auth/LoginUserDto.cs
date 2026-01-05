using FrasesApi.Features.Auth.Application.Commands.Login;

namespace FrasesApi.Data.Auth;

public record LoginUserDto(
    string Email,
    string Password
);

public static class LoginUserDtoToCommand
{
    public static LoginUserCommand MapToCommand(this LoginUserDto dto)
    {
        return new LoginUserCommand
        {
            Email = dto.Email,
            Password = dto.Password
        };
    }
}