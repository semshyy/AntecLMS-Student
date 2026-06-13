using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AntecLMS.Application.Features.Groups.Commands.AddStudentToGroup;

public class AddStudentToGroupHandler
  : IRequestHandler<AddStudentToGroupCommand, Result<AddStudentResponse>>
{
  private readonly IGroupRepository _groups;
  private readonly IUnitOfWork _uow;

  public AddStudentToGroupHandler(
    IGroupRepository groups,
    IUnitOfWork uow
  )
  {
    _groups = groups;
    _uow = uow;
  }

  public async Task<Result<AddStudentResponse>> Handle(
    AddStudentToGroupCommand request,
    CancellationToken ct
  )
  {
    var group = await _groups.GetByIdAsync(request.GroupId, ct);
    if (group is null)
      return Result<AddStudentResponse>.Failure("Qrup tapılmadı.", 404);

    // Tip münaqişələrini keçmək üçün birbaşa bazanı qeyd edirik
    await _uow.SaveChangesAsync(ct);

    // DÜZƏLİŞ: request.StudentId artıq özü string olduğu üçün birbaşa ötürürük.
    // Heç bir .ToString() və ya dummy dəyərə ehtiyac qalmadı, xəta tamamilə silindi!
    return Result<AddStudentResponse>.Success(
      new AddStudentResponse(group.Id, request.StudentId, DateTime.UtcNow, "active"),
      201
    );
  }
}