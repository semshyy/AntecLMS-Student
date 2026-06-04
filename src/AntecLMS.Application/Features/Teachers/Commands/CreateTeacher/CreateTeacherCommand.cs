using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Teachers.Commands.CreateTeacher;

public record CreateTeacherCommand(
  string Name,
  string Surname,
  string Email,
  string Password,
  string? Phone,
  string? Specialization,
  string? Bio,
  string Status
) : IRequest<Result<TeacherResponse>>;

public record TeacherResponse(
  int Id,
  int UserId,
  string Name,
  string Surname,
  string Email,
  string? Specialization,
  string Status
);
