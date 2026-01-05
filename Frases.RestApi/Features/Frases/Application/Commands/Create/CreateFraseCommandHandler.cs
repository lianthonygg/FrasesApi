using FrasesApi.Features.Frases.Domain.Entities;
using FrasesApi.Shared.Application.Common.Abstractions;
using FrasesApi.Shared.Application.Common.ResultsHandler;
using FrasesApi.Shared.Domain.Common;

namespace FrasesApi.Features.Frases.Application.Commands.Create;

public class CreateFraseCommandHandler(IRepository repository): ICommandHandler<CreateFraseCommand, CreateFraseResponseDto>
{
    public async Task<Result<CreateFraseResponseDto>> Handle(CreateFraseCommand command, CancellationToken cancellationToken = default)
    {
        var frase = new Frase
        {
            Description = command.Frase,
            Author = command.Author,
        };
        
        repository.Frases.Add(frase);
        await repository.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(
            Result.Success(new CreateFraseResponseDto(frase.Id.ToString(), frase.Description, frase.Author)));
    }
}