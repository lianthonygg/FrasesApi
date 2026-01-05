using FrasesApi.Features.Auth.Application.Commands.Update;

namespace WebApplication1.Data.Employer;

public record UpdateUserDto(
    string FullName,
    string NickName,
    string Email,
    string Phone
);

public static class UpdateUserDtoToCommand
{
    public static UpdateUserCommand MapToCommand(this UpdateUserDto dto, Guid id)
    {
        return new UpdateUserCommand
        {
            Id = id,
            FullName = dto.FullName,
            NickName = dto.NickName,
            Email = dto.Email,
            Phone = dto.Phone
        };
    }
}