using AntecLMS.Application.Common.Exceptions;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, Result<UpdatedGroupResponse>>
{
  private readonly IGroupRepository _groups;
  private readonly ITeacherRepository _teachers;
  private readonly IUnitOfWork _uow;

  public UpdateGroupHandler(IGroupRepository groups, ITeacherRepository teachers, IUnitOfWork uow)
  {
    _groups = groups;
    _teachers = teachers;
    _uow = uow;
  }

  public async Task<Result<UpdatedGroupResponse>> Handle(
    UpdateGroupCommand request,
    CancellationToken ct
  )
  {
    var group =
      await _groups.GetByIdAsync(request.Id, ct)
      ?? throw new NotFoundException("Group", request.Id);

    var teacher = await _teachers.GetByIdAsync(request.TeacherId, ct);
    if (teacher is null)
      return Result<UpdatedGroupResponse>.Failure("Müəllim tapılmadı.", 404);

    var status = Enum.Parse<GroupStatus>(request.Status, true);
    group.Update(request.Name, request.TeacherId, status);

    _groups.Update(group);
    await _uow.SaveChangesAsync(ct);

    return Result<UpdatedGroupResponse>.Success(
      new UpdatedGroupResponse(group.Id, group.Name, group.TeacherId)
    );
  }
}
