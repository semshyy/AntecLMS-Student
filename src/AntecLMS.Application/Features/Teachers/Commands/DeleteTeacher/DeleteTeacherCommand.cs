using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Teachers.Commands.DeleteTeacher;

public record DeleteTeacherCommand(int Id) : IRequest<Result>;
