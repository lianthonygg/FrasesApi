using FluentValidation;

namespace FrasesApi.Features.Frases.Application.Commands.Create;

public class CreateFraseCommandValidator: AbstractValidator<CreateFraseCommand>
{
    public CreateFraseCommandValidator()
    {
        RuleFor(command => command.Frase).NotNull().NotEmpty();
        RuleFor(command => command.Author).NotNull().NotEmpty();
    }
}