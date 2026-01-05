using FluentValidation;

namespace FrasesApi.Features.Auth.Application.Commands.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FullName)
          .NotEmpty().WithMessage("Name is required")
          .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
          .Matches("^[a-zA-Z ]*$").WithMessage("Name can only contain letters and spaces");

        RuleFor(x => x.Nickname)
          .MinimumLength(3).WithMessage("Nickname must be at least 3 characters")
          .MaximumLength(50).WithMessage("Nickname must not exceed 50 characters")
          .Matches("^[a-zA-Z0-9_-]*$").WithMessage("Nickname can only contain letters, numbers, underscores and hyphens")
          .When(x => x.Nickname != null);
         
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

        RuleFor(x => x.Password)
          .NotEmpty().WithMessage("Password is required")
          .MinimumLength(8).WithMessage("Password must be at least 8 characters")
          .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
          .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
          .Matches("[0-9]").WithMessage("Password must contain at least one number")
          .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
    }
}
