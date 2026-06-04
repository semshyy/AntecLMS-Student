using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Teachers.Commands.CreateTeacher;

public class CreateTeacherHandler : IRequestHandler<CreateTeacherCommand, Result<TeacherResponse>>
{
  private readonly IUserRepository _users;
  private readonly ITeacherRepository _teachers;
  private readonly IUnitOfWork _uow;
  private readonly IPasswordHasher _hasher;

  public CreateTeacherHandler(
    IUserRepository users,
    ITeacherRepository teachers,
    IUnitOfWork uow,
    IPasswordHasher hasher
  )
  {
    _users = users;
    _teachers = teachers;
    _uow = uow;
    _hasher = hasher;
  }

  public async Task<Result<TeacherResponse>> Handle(
    CreateTeacherCommand request,
    CancellationToken ct
  )
  {
    if (await _users.EmailExistsAsync(request.Email, ct))
      return Result<TeacherResponse>.Failure("Bu email artıq mövcuddur.", 422);

    var status = Enum.Parse<UserStatus>(request.Status, true);

    var user = User.Create(
      request.Name,
      request.Surname,
      request.Email,
      _hasher.Hash(request.Password),
      UserRole.Teacher,
      request.Phone,
      status
    );

    await _users.AddAsync(user, ct);
    await _uow.SaveChangesAsync(ct);

    var teacher = Teacher.Create(user.Id, request.Specialization, request.Bio, status);
    await _teachers.AddAsync(teacher, ct);
    await _uow.SaveChangesAsync(ct);

    return Result<TeacherResponse>.Success(
      new TeacherResponse(
        teacher.Id,
        user.Id,
        user.Name,
        user.Surname,
        user.Email,
        teacher.Specialization,
        teacher.Status.ToString().ToLower()
      ),
      201
    );
  }
}
