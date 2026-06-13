using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Enums;
using AntecLMS.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AntecLMS.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Result<CourseResponse>>
{
  private readonly ICourseRepository _courses;
  private readonly IUnitOfWork _uow;

  public CreateCourseHandler(ICourseRepository courses, IUnitOfWork uow)
  {
    _courses = courses;
    _uow = uow;
  }

  public async Task<Result<CourseResponse>> Handle(
    CreateCourseCommand request,
    CancellationToken ct
  )
  {
    var status = Enum.Parse<CourseStatus>(request.Status, true);

    // DÜZƏLİŞ: Yeni əlavə etdiyimiz StartDate və EndDate parametrləri bura keçirildi.
    // Əgər request (Command) daxilində hələ bu tarixlər yoxdursa, müvəqqəti olaraq 
    // DateTime.UtcNow və DateTime.UtcNow.AddMonths(3) yazıb xətanı keçə bilərik.
    var course = Course.Create(
         request.Name,
         request.Description,
         status,
         DateTime.UtcNow,              // request-dən oxumaq əvəzinə birbaşa indiki zamanı qoyuruq
         DateTime.UtcNow.AddMonths(3)  // Kursun bitmə tarixini də standart olaraq 3 ay sonraya təyin edirik
     );
    await _courses.AddAsync(course, ct);
    await _uow.SaveChangesAsync(ct);

    return Result<CourseResponse>.Success(
      new CourseResponse(
        course.Id,
        course.Name,
        course.Description,
        course.Status.ToString().ToLower(),
        course.CreatedAt
      ),
      201
    );
  }
}