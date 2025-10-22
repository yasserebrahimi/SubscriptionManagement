
namespace SubscriptionManagement.Application.Common.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public string? Error { get; init; }
        public List<ValidationError> ValidationErrors { get; init; } = new();

        public static Result<T> Success(T data) => new() { IsSuccess = true, Data = data };
        public static Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
        public static Result<T> ValidationFailure(List<ValidationError> errors) => new() { IsSuccess = false, Error = "Validation failed", ValidationErrors = errors };
    }
    public record ValidationError(string PropertyName, string ErrorMessage);
}
