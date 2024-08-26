using System.Text.Json.Serialization;

namespace B3Case.Core.Schema
{
    public class Result : Result<object>
    {
        public Result() { }
    }

    public class Result<T>
    {
        public bool IsSuccess { get; set; } = true;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int StatusCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }

        public void SetError(string errorMessage)
        {
            IsSuccess = false;
            StatusCode = 500;
            ErrorMessage = errorMessage;
        }

        public void SetSuccess(T data)
        {
            IsSuccess = true;
            StatusCode = 200;
            Data = data;
        }

        public void SetSuccess()
        {
            IsSuccess = true;
            StatusCode = 200;
        }

        public void SetCreate()
        {
            IsSuccess = true;
            StatusCode = 201;
        }
    }
}
