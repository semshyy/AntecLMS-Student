using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(int Id, string Name, string Surname, string? Phone, string Status)
  : IRequest<Result<UpdatedUserResponse>>;

public record UpdatedUserResponse(
  int Id,
  string Name,
  string Surname,
  string? Phone,
  string Status
);
