using System.Diagnostics.CodeAnalysis;
using ApplicationException = FrasesApi.Shared.Application.Common.Exceptions.ApplicationException;

public sealed class NullValueException : ApplicationException
    {
        public NullValueException() : base("Null value was provided.") { }
        public override string ErrorCode => "Error.NullValue";
    }

namespace FrasesApi.Shared.Application.Common.ResultsHandler
{

    public sealed class Error
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; }
        public string Description { get; }
    }

    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None) throw new InvalidOperationException();
            if (!isSuccess && error == Error.None) throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
            IsFailure = !isSuccess;
        }
        
        public bool IsSuccess { get; }
        public bool IsFailure { get; }
        public Error Error { get; }

        public static Result Success() => new(true, Error.None);
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        public static Result Failure(Error error) => new(false, error);
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
        
        public static Result Failure(FrasesApi.Shared.Application.Common.Exceptions.ApplicationException exception) =>
            new(false, new Error(exception.ErrorCode, exception.Message));
        public static Result<TValue> Failure<TValue>(FrasesApi.Shared.Application.Common.Exceptions.ApplicationException exception) =>
            new Result<TValue>(default, false, new Error(exception.ErrorCode, exception.Message));
        
        public static Result<TValue> Create<TValue>(TValue? value) =>
            value is null
                ? Failure<TValue>(new NullValueException())
                : Success(value);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        protected internal Result(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        [NotNull]
        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }
}
