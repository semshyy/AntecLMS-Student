using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
  private readonly IJwtService _jwt;

  public LogoutCommandHandler(IJwtService jwt) => _jwt = jwt;

  public Task<Result> Handle(LogoutCommand request, CancellationToken ct)
  {
    _jwt.InvalidateToken(request.Token);
    return Task.FromResult(Result.Success());
  }
}
