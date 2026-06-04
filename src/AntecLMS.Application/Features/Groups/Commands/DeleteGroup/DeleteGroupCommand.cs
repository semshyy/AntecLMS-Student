using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Groups.Commands.DeleteGroup;

public record DeleteGroupCommand(int Id) : IRequest<Result>;
