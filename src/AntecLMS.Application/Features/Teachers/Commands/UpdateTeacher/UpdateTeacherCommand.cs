using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Teachers.Commands.UpdateTeacher;

public record UpdateTeacherCommand(int Id, string? Phone, string? Specialization, string Status)
  : IRequest<Result<UpdatedTeacherResponse>>;

public record UpdatedTeacherResponse(int Id, string? Specialization, string Status);
