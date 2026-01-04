namespace FrasesApi.Shared.Domain.Common;

public class Error(string errorCode, string message) : Exception(message)
{
    public string ErrorCode { get; set; } = errorCode;
}