using FrasesApi.Features.Frases.Domain.Entities;

namespace FrasesApi.Features.Frases.Application.Queries.GetAll;

public record GetAllFrasesResponseDto(string Id, string Frase, string Author);

public static class GetAllFrasesToResponse
{
    public static GetAllFrasesResponseDto MapToResponse(Frase frase)
    {
        return new GetAllFrasesResponseDto(frase.Id.ToString(), frase.Description, frase.Author);
    }
}