using AntecLMS.Application.Common.Exceptions;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AntecLMS.Application.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseHandler
  : IRequestHandler<UpdateCourseCommand, Result<UpdatedCourseResponse>>
{
  private readonly ICourseRepository _courses;
  private readonly IUnitOfWork _uow;

  public UpdateCourseHandler(ICourseRepository courses, IUnitOfWork uow)
  {
    _courses = courses;
    _uow = uow;
  }

  public async Task<Result<UpdatedCourseResponse>> Handle(
    UpdateCourseCommand request,
    CancellationToken ct
  )
  {
    var course =
      await _courses.GetByIdAsync(request.Id, ct)
      ?? throw new NotFoundException("Course", request.Id);

    var status = Enum.Parse<CourseStatus>(request.Status, true);

    // DÜZƏLİŞ: Yeni əlavə etdiyimiz StartDate və EndDate parametrləri metodu qırmasın deyə,
    // kursun öz daxilində olan mövcud tarixlərini bura ötürürük.
    course.Update(request.Name, request.Description, status, course.StartDate, course.EndDate);

    _courses.Update(course);
    await _uow.SaveChangesAsync(ct);

    return Result<UpdatedCourseResponse>.Success(
      new UpdatedCourseResponse(course.Id, course.Name, course.Status.ToString().ToLower())
    );
  }
}