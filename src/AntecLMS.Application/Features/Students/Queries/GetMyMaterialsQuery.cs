using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyMaterials;

public record GetMyMaterialsQuery : IRequest<Result<List<MyMaterialDetail>>>;

public record MyMaterialDetail(
    int Id,
    string FileName,
    string? Description,
    string Type,
    string FilePath,
    string LessonTitle,
    DateOnly LessonDate
);