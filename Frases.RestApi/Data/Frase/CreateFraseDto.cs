using FrasesApi.Features.Frases.Application.Commands.Create;

namespace FrasesApi.Data.Frase;

public record CreateFraseDto(
    string Frase,
    string Author
);


public static class CreateFraseToCommand
{
    public static CreateFraseCommand MapToCommand(this CreateFraseDto dto)
    {
        return new CreateFraseCommand
        {
            Frase = dto.Frase,
            Author = dto.Author
        };
    }
}