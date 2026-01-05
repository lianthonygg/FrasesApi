using FluentValidation;

namespace FrasesApi.Features.Auth.Application.Commands.Update;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
            .Matches("^[a-zA-Z ]*$").WithMessage("Name can only contain letters and spaces");

        RuleFor(x => x.NickName)
            .MinimumLength(3).WithMessage("Nickname must be at least 3 characters")
            .MaximumLength(50).WithMessage("Nickname must not exceed 50 characters")
            .Matches("^[a-zA-Z0-9_-]*$").WithMessage("Nickname can only contain letters, numbers, underscores and hyphens")
            .When(x => x.NickName != null);
         
        RuleFor(x => x.Phone)
            .MinimumLength(7).WithMessage("Phone number must be at least 7 digits")
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters")
            .Matches("^[0-9]*$").WithMessage("Phone number can only contain numbers")
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters")
            .When(x => x.Phone != null); 

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters");
    }
}