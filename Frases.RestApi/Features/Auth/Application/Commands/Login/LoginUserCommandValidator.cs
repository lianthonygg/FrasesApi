using FluentValidation;

namespace FrasesApi.Features.Auth.Application.Commands.Login;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Email).EmailAddress().NotEmpty();
        RuleFor(command => command.Password).NotEmpty().MinimumLength(6);
    }
}