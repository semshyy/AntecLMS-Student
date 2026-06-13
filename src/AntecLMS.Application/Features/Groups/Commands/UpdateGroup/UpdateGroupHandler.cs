using AntecLMS.Application.Common.Exceptions;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AntecLMS.Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, Result<UpdatedGroupResponse>>
{
  private readonly IGroupRepository _groups;
  private readonly IUnitOfWork _uow;

  public UpdateGroupHandler(IGroupRepository groups, IUnitOfWork uow)
  {
    _groups = groups;
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

    var status = Enum.Parse<GroupStatus>(request.Status, true);

    group.Update(group.GroupCode, request.Name, request.TeacherId, status);

    _groups.Update(group);
    await _uow.SaveChangesAsync(ct);

    return Result<UpdatedGroupResponse>.Success(
        new UpdatedGroupResponse(group.Id, group.Name, group.TeacherId)
    );
  }
}