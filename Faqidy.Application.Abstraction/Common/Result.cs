namespace Faqidy.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public Result(bool isSuccess , string errorMessage , T data)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Data = data;
        }

        public static Result<T> Success(T data) => new (true , string.Empty , data);
        public static Result<T> Failure(string errorMessage) => new(false, errorMessage, default(T)!);

    }
}
