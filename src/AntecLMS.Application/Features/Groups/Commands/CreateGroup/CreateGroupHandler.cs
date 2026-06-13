using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AntecLMS.Application.Features.Groups.Commands.CreateGroup;

public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, Result<GroupResponse>>
{
  private readonly IGroupRepository _groups;
  private readonly ICourseRepository _courses;
  private readonly IUnitOfWork _uow;

  public CreateGroupHandler(IGroupRepository groups, ICourseRepository courses, IUnitOfWork uow)
  {
    _groups = groups;
    _courses = courses;
    _uow = uow;
  }

  public async Task<Result<GroupResponse>> Handle(CreateGroupCommand request, CancellationToken ct)
  {
    var course = await _courses.GetByIdAsync(request.CourseId, ct);
    if (course is null)
      return Result<GroupResponse>.Failure("Kurs tapılmadı.", 404);

    var status = Enum.Parse<GroupStatus>(request.Status, true);
    string generatedGroupCode = $"GRP-{new Random().Next(100, 999)}";

    var group = Group.Create(
        generatedGroupCode,
        request.Name,
        request.CourseId,
        request.TeacherId,  // ← birbaşa int, .ToString() yox
        request.StartDate,
        request.EndDate,
        status
    );

    await _groups.AddAsync(group, ct);
    await _uow.SaveChangesAsync(ct);

    return Result<GroupResponse>.Success(
        new GroupResponse(
            group.Id,
            group.Name,
            group.CourseId,
            group.TeacherId,  // ← 0 yox, real dəyər
            group.StartDate,
            group.Status.ToString().ToLower()
        ),
        201
    );
  }
}