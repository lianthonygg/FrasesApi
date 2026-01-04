namespace FrasesApi.Shared.Application.Common.Exceptions;

public class ValidationException(IReadOnlyCollection<ValidationError> errors)
    : ApplicationException(message: "One or more validation errors have occurred")
{
    public override string ErrorCode => "ValidationException";

    public IReadOnlyCollection<ValidationError> Errors { get; set; } = errors;
}

public record ValidationError(string PropertyName, string ErrorMessage);