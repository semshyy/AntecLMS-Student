using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Groups.Commands.CreateGroup;

public record CreateGroupCommand(
  string Name,
  int CourseId,
  int TeacherId,
  DateOnly StartDate,
  DateOnly? EndDate,
  string Status
) : IRequest<Result<GroupResponse>>;

public record GroupResponse(
  int Id,
  string Name,
  int CourseId,
  int TeacherId,
  DateOnly StartDate,
  string Status
);
