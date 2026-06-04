using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<LoginResponse>>;

public record LoginResponse(string Token, UserDto User);

public record UserDto(
  int Id,
  string Name,
  string Surname,
  string Email,
  string Role,
  string Status
);
