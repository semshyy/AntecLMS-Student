using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyAttendance;

public record GetMyAttendanceQuery : IRequest<Result<List<MyAttendanceItem>>>;

public record MyAttendanceItem(
    int Id,
    DateOnly LessonDate,
    string LessonTitle,
    string Status,
    int? LateMinutes,
    string? Reason
);