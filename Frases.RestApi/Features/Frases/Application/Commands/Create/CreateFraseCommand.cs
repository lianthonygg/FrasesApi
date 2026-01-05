using FrasesApi.Shared.Application.Common.Abstractions;

namespace FrasesApi.Features.Frases.Application.Commands.Create;

public class CreateFraseCommand: ICommand<CreateFraseResponseDto>
{
    public required string Frase { get; init; }
    public required string Author { get; init; }
}