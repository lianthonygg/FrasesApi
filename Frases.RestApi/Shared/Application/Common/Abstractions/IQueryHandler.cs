using FrasesApi.Shared.Application.Common.ResultsHandler;

namespace FrasesApi.Shared.Application.Common.Abstractions;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken = default);
}