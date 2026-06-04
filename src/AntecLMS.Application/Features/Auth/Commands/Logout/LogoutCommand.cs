using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Auth.Commands.Logout;

public record LogoutCommand(string Token) : IRequest<Result>;
