using System.Security.Claims;
using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AntecLMS.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
  private readonly IHttpContextAccessor _http;

  public CurrentUserService(IHttpContextAccessor http) => _http = http;

  public int UserId =>
    int.Parse(_http.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

  public UserRole Role =>
    Enum.Parse<UserRole>(_http.HttpContext!.User.FindFirstValue(ClaimTypes.Role)!);

  public string Email => _http.HttpContext!.User.FindFirstValue(ClaimTypes.Email)!;
}
