using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Courses.Commands.DeleteCourse;

public record DeleteCourseCommand(int Id) : IRequest<Result>;
