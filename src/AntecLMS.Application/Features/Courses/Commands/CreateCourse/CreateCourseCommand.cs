using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Courses.Commands.CreateCourse;

public record CreateCourseCommand(string Name, string? Description, string Status)
  : IRequest<Result<CourseResponse>>;

public record CourseResponse(
  int Id,
  string Name,
  string? Description,
  string Status,
  DateTime CreatedAt
);
