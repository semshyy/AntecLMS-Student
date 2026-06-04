using AntecLMS.Application.Common.Exceptions;
using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Auth.Queries.GetMe;

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, Result<MeResponse>>
{
  private readonly IUserRepository _users;
  private readonly ICurrentUserService _current;

  public GetMeQueryHandler(IUserRepository users, ICurrentUserService current)
  {
    _users = users;
    _current = current;
  }

  public async Task<Result<MeResponse>> Handle(GetMeQuery request, CancellationToken ct)
  {
    var user =
      await _users.GetByIdAsync(_current.UserId, ct)
      ?? throw new NotFoundException("User", _current.UserId);

    return Result<MeResponse>.Success(
      new MeResponse(
        user.Id,
        user.Name,
        user.Surname,
        user.Email,
        user.Role.ToString().ToLower(),
        user.Phone,
        user.Status.ToString().ToLower()
      )
    );
  }
}
