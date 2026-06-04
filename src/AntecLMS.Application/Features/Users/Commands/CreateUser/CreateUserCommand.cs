using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
  string Name,
  string Surname,
  string Email,
  string Password,
  string Role,
  string? Phone,
  string Status
) : IRequest<Result<CreatedUserResponse>>;

public record CreatedUserResponse(
  int Id,
  string Name,
  string Surname,
  string Email,
  string Role,
  string Status,
  DateTime CreatedAt
);
