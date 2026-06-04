using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Groups.Commands.UpdateGroup;

public record UpdateGroupCommand(int Id, string Name, int TeacherId, string Status)
  : IRequest<Result<UpdatedGroupResponse>>;

public record UpdatedGroupResponse(int Id, string Name, int TeacherId);
