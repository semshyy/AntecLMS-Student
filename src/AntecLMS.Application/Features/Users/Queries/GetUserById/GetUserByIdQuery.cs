using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<Result<UserDetailResponse>>;

public record UserDetailResponse(
  int Id,
  string Name,
  string Surname,
  string Email,
  string Role,
  string? Phone,
  string Status,
  DateTime CreatedAt,
  DateTime? UpdatedAt
);
