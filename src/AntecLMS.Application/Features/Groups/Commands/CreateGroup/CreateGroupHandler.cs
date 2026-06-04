using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Groups.Commands.CreateGroup;

public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, Result<GroupResponse>>
{
  private readonly IGroupRepository _groups;
  private readonly ICourseRepository _courses;
  private readonly ITeacherRepository _teachers;
  private readonly IUnitOfWork _uow;

  public CreateGroupHandler(
    IGroupRepository groups,
    ICourseRepository courses,
    ITeacherRepository teachers,
    IUnitOfWork uow
  )
  {
    _groups = groups;
    _courses = courses;
    _teachers = teachers;
    _uow = uow;
  }

  public async Task<Result<GroupResponse>> Handle(CreateGroupCommand request, CancellationToken ct)
  {
    var course = await _courses.GetByIdAsync(request.CourseId, ct);
    if (course is null)
      return Result<GroupResponse>.Failure("Kurs tapılmadı.", 404);

    var teacher = await _teachers.GetByIdAsync(request.TeacherId, ct);
    if (teacher is null)
      return Result<GroupResponse>.Failure("Müəllim tapılmadı.", 404);

    var status = Enum.Parse<GroupStatus>(request.Status, true);
    var group = Group.Create(
      request.Name,
      request.CourseId,
      request.TeacherId,
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
        group.TeacherId,
        group.StartDate,
        group.Status.ToString().ToLower()
      ),
      201
    );
  }
}
