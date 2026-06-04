using AntecLMS.Application.Common.Exceptions;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Teachers.Commands.UpdateTeacher;

public class UpdateTeacherHandler
  : IRequestHandler<UpdateTeacherCommand, Result<UpdatedTeacherResponse>>
{
  private readonly ITeacherRepository _teachers;
  private readonly IUnitOfWork _uow;

  public UpdateTeacherHandler(ITeacherRepository teachers, IUnitOfWork uow)
  {
    _teachers = teachers;
    _uow = uow;
  }

  public async Task<Result<UpdatedTeacherResponse>> Handle(
    UpdateTeacherCommand request,
    CancellationToken ct
  )
  {
    var teacher =
      await _teachers.GetWithUserAsync(request.Id, ct)
      ?? throw new NotFoundException("Teacher", request.Id);

    var status = Enum.Parse<UserStatus>(request.Status, true);
    teacher.Update(request.Specialization, teacher.Bio, status);

    if (request.Phone is not null)
    {
      teacher.User.Update(teacher.User.Name, teacher.User.Surname, request.Phone, status);
    }

    _teachers.Update(teacher);
    await _uow.SaveChangesAsync(ct);

    return Result<UpdatedTeacherResponse>.Success(
      new UpdatedTeacherResponse(
        teacher.Id,
        teacher.Specialization,
        teacher.Status.ToString().ToLower()
      )
    );
  }
}
