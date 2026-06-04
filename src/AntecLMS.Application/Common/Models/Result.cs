namespace AntecLMS.Application.Common.Models;

public class Result<T>
{
  public bool IsSuccess { get; private set; }
  public T? Data { get; private set; }
  public string? Error { get; private set; }
  public IDictionary<string, string[]>? Errors { get; private set; }
  public int StatusCode { get; private set; }

  public static Result<T> Success(T data, int code = 200) =>
    new()
    {
      IsSuccess = true,
      Data = data,
      StatusCode = code,
    };

  public static Result<T> Failure(
    string error,
    int code = 400,
    IDictionary<string, string[]>? errors = null
  ) =>
    new()
    {
      IsSuccess = false,
      Error = error,
      StatusCode = code,
      Errors = errors,
    };
}

public class Result
{
  public bool IsSuccess { get; private set; }
  public string? Error { get; private set; }
  public int StatusCode { get; private set; }
  public IDictionary<string, string[]>? Errors { get; private set; }

  public static Result Success(int code = 200) => new() { IsSuccess = true, StatusCode = code };

  public static Result Failure(
    string error,
    int code = 400,
    IDictionary<string, string[]>? errors = null
  ) =>
    new()
    {
      IsSuccess = false,
      Error = error,
      StatusCode = code,
      Errors = errors,
    };
}
