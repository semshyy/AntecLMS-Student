using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Auth.Queries.GetMe;

public record GetMeQuery : IRequest<Result<MeResponse>>;

public record MeResponse(
  int Id,
  string Name,
  string Surname,
  string Email,
  string Role,
  string? Phone,
  string Status
);
