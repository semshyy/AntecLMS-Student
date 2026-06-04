using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Result<StudentResponse>>
{
  private readonly IUserRepository _users;
  private readonly IStudentRepository _students;
  private readonly IUnitOfWork _uow;
  private readonly IPasswordHasher _hasher;

  public CreateStudentHandler(
    IUserRepository users,
    IStudentRepository students,
    IUnitOfWork uow,
    IPasswordHasher hasher
  )
  {
    _users = users;
    _students = students;
    _uow = uow;
    _hasher = hasher;
  }

  public async Task<Result<StudentResponse>> Handle(
    CreateStudentCommand request,
    CancellationToken ct
  )
  {
    if (await _users.EmailExistsAsync(request.Email, ct))
      return Result<StudentResponse>.Failure("Bu email artıq mövcuddur.", 422);

    var status = Enum.Parse<UserStatus>(request.Status, true);

    var user = User.Create(
      request.Name,
      request.Surname,
      request.Email,
      _hasher.Hash(request.Password),
      UserRole.Student,
      request.Phone,
      status
    );

    await _users.AddAsync(user, ct);
    await _uow.SaveChangesAsync(ct);

    var student = Student.Create(user.Id, request.BirthDate, request.Note, status);
    await _students.AddAsync(student, ct);
    await _uow.SaveChangesAsync(ct);

    return Result<StudentResponse>.Success(
      new StudentResponse(
        student.Id,
        user.Id,
        user.Name,
        user.Surname,
        user.Email,
        student.Status.ToString().ToLower()
      ),
      201
    );
  }
}
