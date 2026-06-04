using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Commands.UpdateStudent;

public record UpdateStudentCommand(int Id, string? Phone, string? Note, string Status)
  : IRequest<Result<UpdatedStudentResponse>>;

public record UpdatedStudentResponse(int Id, string? Phone, string? Note, string Status);
