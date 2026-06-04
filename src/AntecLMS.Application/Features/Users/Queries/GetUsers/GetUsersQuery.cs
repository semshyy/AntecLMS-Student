using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery(
  string? Role,
  string? Status,
  string? Search,
  int Page = 1,
  int PerPage = 20
) : IRequest<Result<PagedResult<UserListItem>>>;

public record UserListItem(
  int Id,
  string Name,
  string Surname,
  string Email,
  string Role,
  string? Phone,
  string Status,
  DateTime CreatedAt
);
