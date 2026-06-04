using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Commands.DeleteStudent;

public record DeleteStudentCommand(int Id) : IRequest<Result>;
