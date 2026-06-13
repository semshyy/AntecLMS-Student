using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyLessons;

public record GetMyLessonsQuery : IRequest<Result<List<MyLessonItem>>>;

public record MyLessonItem(
    int Id,
    string Title,
    string? Description,
    DateOnly LessonDate,
    string GroupName,
    List<MyMaterialItem> Materials
);

public record MyMaterialItem(
    int Id,
    string FileName,
    string? Description,
    string Type,
    string FilePath
);