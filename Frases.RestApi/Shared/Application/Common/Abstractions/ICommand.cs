namespace FrasesApi.Shared.Application.Common.Abstractions;

public interface IBaseCommand;

public interface ICommand : IBaseCommand;
public interface ICommand<in TResponse> : IBaseCommand;