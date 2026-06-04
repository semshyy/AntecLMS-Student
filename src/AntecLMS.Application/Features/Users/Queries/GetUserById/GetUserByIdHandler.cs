using AntecLMS.Application.Common.Exceptions;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserDetailResponse>>
{
  private readonly IUserRepository _users;

  public GetUserByIdHandler(IUserRepository users) => _users = users;

  public async Task<Result<UserDetailResponse>> Handle(
    GetUserByIdQuery request,
    CancellationToken ct
  )
  {
    var user =
      await _users.GetByIdAsync(request.Id, ct) ?? throw new NotFoundException("User", request.Id);

    return Result<UserDetailResponse>.Success(
      new UserDetailResponse(
        user.Id,
        user.Name,
        user.Surname,
        user.Email,
        user.Role.ToString().ToLower(),
        user.Phone,
        user.Status.ToString().ToLower(),
        user.CreatedAt,
        user.UpdatedAt
      )
    );
  }
}
