using FrasesApi.Shared.Application.Common.Abstractions;
using FrasesApi.Shared.Application.Common.ResultsHandler;
using FrasesApi.Shared.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace FrasesApi.Features.Frases.Application.Queries.GetAll;

public class GetAllFrasesQueryHandler(IRepository repository): IQueryHandler<GetAllFrasesQuery, List<GetAllFrasesResponseDto>>
{
    public async Task<Result<List<GetAllFrasesResponseDto>>> Handle(GetAllFrasesQuery query, CancellationToken cancellationToken = default)
    {
        var frases = await repository.Frases.ToListAsync(cancellationToken);

        var result = frases.Select(GetAllFrasesToResponse.MapToResponse).ToList();
        
        return await Task.FromResult(Result.Success(result));
    }
}