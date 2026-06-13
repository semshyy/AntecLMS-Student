using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyGrades;

public record GetMyGradesQuery : IRequest<Result<List<MyGradeItem>>>;

public record MyGradeItem(
    int Id,
    string LessonTitle,
    DateOnly LessonDate,
    double Score,
    double MaxScore,
    string? Comment
);