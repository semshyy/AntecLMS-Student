using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Groups.Commands.RemoveStudentFromGroup;

public record RemoveStudentFromGroupCommand(int GroupId, int StudentId) : IRequest<Result>;
