using FrasesApi.Shared.Application.Common.Abstractions;

namespace FrasesApi.Features.Frases.Application.Queries.GetAll;

public class GetAllFrasesQuery: IQuery<List<GetAllFrasesResponseDto>>;