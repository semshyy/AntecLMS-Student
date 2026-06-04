using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Commands.CreateStudent;

public record CreateStudentCommand(
  string Name,
  string Surname,
  string Email,
  string Password,
  string? Phone,
  DateOnly? BirthDate,
  string? Note,
  string Status
) : IRequest<Result<StudentResponse>>;

public record StudentResponse(
  int Id,
  int UserId,
  string Name,
  string Surname,
  string Email,
  string Status
);
