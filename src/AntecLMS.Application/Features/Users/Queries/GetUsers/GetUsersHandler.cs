using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Users.Queries.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, Result<PagedResult<UserListItem>>>
{
  private readonly IUserRepository _users;

  public GetUsersHandler(IUserRepository users) => _users = users;

  public async Task<Result<PagedResult<UserListItem>>> Handle(
    GetUsersQuery request,
    CancellationToken ct
  )
  {
    UserRole? role = request.Role is null ? null : Enum.Parse<UserRole>(request.Role, true);
    UserStatus? status = request.Status is null
      ? null
      : Enum.Parse<UserStatus>(request.Status, true);

    var (items, total) = await _users.GetPagedAsync(
      role,
      status,
      request.Search,
      request.Page,
      request.PerPage,
      ct
    );

    var data = items
      .Select(u => new UserListItem(
        u.Id,
        u.Name,
        u.Surname,
        u.Email,
        u.Role.ToString().ToLower(),
        u.Phone,
        u.Status.ToString().ToLower(),
        u.CreatedAt
      ))
      .ToList();

    return Result<PagedResult<UserListItem>>.Success(
      new PagedResult<UserListItem>
      {
        Data = data,
        Total = total,
        Page = request.Page,
        PerPage = request.PerPage,
      }
    );
  }
}
