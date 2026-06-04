using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<Result>;
