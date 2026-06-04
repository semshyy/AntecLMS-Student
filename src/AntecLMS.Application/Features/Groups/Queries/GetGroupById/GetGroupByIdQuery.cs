using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Groups.Queries.GetGroupById;

public record GetGroupByIdQuery(int Id) : IRequest<Result<GroupDetailResponse>>;

public record GroupDetailResponse(
  int Id,
  string Name,
  CourseInfo Course,
  TeacherInfo Teacher,
  DateOnly StartDate,
  DateOnly? EndDate,
  string Status,
  List<StudentInGroup> Students,
  int StudentsCount
);

public record CourseInfo(int Id, string Name);

public record TeacherInfo(int Id, string Name, string Surname);

public record StudentInGroup(int Id, string Name, string Surname, string Status);
