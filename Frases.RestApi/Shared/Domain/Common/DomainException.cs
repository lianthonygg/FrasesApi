namespace FrasesApi.Shared.Domain.Common;

public class DomainException(string errorCode, string message) : Error(errorCode, message);