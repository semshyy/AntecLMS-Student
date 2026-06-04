namespace AntecLMS.Application.Common.Models;

public class PagedResult<T>
{
  public List<T> Data { get; init; } = new();
  public int Total { get; init; }
  public int Page { get; init; }
  public int PerPage { get; init; }
}
