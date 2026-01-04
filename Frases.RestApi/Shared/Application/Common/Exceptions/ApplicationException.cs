namespace FrasesApi.Shared.Application.Common.Exceptions;

public abstract class ApplicationException(string message) : Exception(message)
{
    public abstract string ErrorCode { get; }
}