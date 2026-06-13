using AntecLMS.Application.Common.Exceptions;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AntecLMS.Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Result<UpdatedStudentResponse>>
{
  private readonly IStudentRepository _students;
  private readonly IUnitOfWork _uow;

  public UpdateStudentHandler(IStudentRepository students, IUnitOfWork uow)
  {
    _students = students;
    _uow = uow;
  }

  public async Task<Result<UpdatedStudentResponse>> Handle(UpdateStudentCommand request, CancellationToken ct)
  {
    Student student = await _students.GetWithUserAsync(request.Id, ct)
                      ?? throw new NotFoundException("Student", request.Id);

    var status = Enum.Parse<UserStatus>(request.Status, true);

    student.Update(request.Phone, request.Note, status, request.BirthDate);

    if (request.Phone is not null)
    {
      student.User.Update(student.User.Name, student.User.Surname, request.Phone, status);
    }

    _students.Update(student);
    await _uow.SaveChangesAsync(ct);

    return Result<UpdatedStudentResponse>.Success(
        new UpdatedStudentResponse(
            student.Id,
            student.User.Phone,
            student.Note,
            student.Status.ToString().ToLower()
        )
    );
  }
}