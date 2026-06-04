using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetStudentById;

public record GetStudentByIdQuery(int Id) : IRequest<Result<StudentDetailResponse>>;

public record StudentDetailResponse(
  int Id,
  int UserId,
  string Name,
  string Surname,
  string Email,
  string? Phone,
  DateOnly? BirthDate,
  string? Note,
  string Status,
  List<GroupRefD> Groups
);

public record GroupRefD(int Id, string Name);
