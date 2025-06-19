namespace circutBoardWorkFlow.Models
{
    public class Result<T>
    {
        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        private Result(T value)
        {
            Value = value;
            IsSuccess = true;
            Error = Error.None;
        }


        public T? Value { get; }
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        public static Result<T> Success(T value) => new(value);

        public static Result<T> Failure(Error error) => new(false, error);
    }

    public static class ResultExtensions
    {
        public static T Match<T>(
            this Result<T> result,
            Func<T, T> onSuccess,
            Func<Error, T> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value!) : onFailure(result.Error);
        }
    }

    public sealed record Error(int StatusCode, string Title, string Description)
    {
        public static Error None => new(-1, string.Empty, string.Empty);
    }
}
