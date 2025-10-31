namespace WebApp.Application.Models;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }

    private Result(
        bool isSuccess,
        T data,
        List<string> errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Errors = errors;
    }

    public static Result<T> Succuss(T result)
    {
        return new Result<T>(true, result, new List<string>());
    }

    public static Result<T> Failure(List<string> errros)
    {
        return new Result<T>(false, default!, errros);
    }
}
