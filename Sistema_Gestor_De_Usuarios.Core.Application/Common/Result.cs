

namespace Sistema_Gestor_De_Usuarios.Core.Application.Common
{
    public class Result<T>
    {
        public T Value { get; }
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }
        private Result(T value, bool isSuccess, string errorMessage)
        {
            Value = value;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;

        }

        //Factory methods
        public static Result<T> Success(T value) => new(value, true, "");
        public static Result<T> Failure(string errorMessage) => new(default!, false, errorMessage);
    }
}
