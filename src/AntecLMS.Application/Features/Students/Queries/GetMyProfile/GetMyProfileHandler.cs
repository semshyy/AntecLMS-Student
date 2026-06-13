using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyProfile;

public class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, Result<MyProfileResponse>>
{
  private readonly IStudentRepository _students;
  private readonly ICurrentUserService _currentUser;

  public GetMyProfileHandler(IStudentRepository students, ICurrentUserService currentUser)
  {
    _students = students;
    _currentUser = currentUser;
  }

  public async Task<Result<MyProfileResponse>> Handle(GetMyProfileQuery request, CancellationToken ct)
  {
    var student = await _students.GetByUserIdAsync(_currentUser.UserId, ct);
    if (student is null)
      return Result<MyProfileResponse>.Failure("Tələbə tapılmadı.", 404);

    return Result<MyProfileResponse>.Success(new MyProfileResponse(
        student.Id,
        student.User.Name,
        student.User.Surname,
        student.User.Email,
        student.Phone,
        student.BirthDate,
        student.Note,
        student.Status.ToString().ToLower()
    ));
  }
}