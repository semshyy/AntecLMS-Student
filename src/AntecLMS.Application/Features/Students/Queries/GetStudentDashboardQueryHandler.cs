using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AntecLMS.Domain.Entities;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries;

// ==========================================
// 1. SORĞU (QUERY) VƏ DTO MODELLƏRİ
// ==========================================
public record GetStudentDashboardQuery(string StudentId) : IRequest<StudentDashboardDto>;

public record StudentDashboardDto(
    string GroupName,
    string CourseName,
    List<RecentMaterialDto> RecentMaterials,
    List<RecentGradeDto> RecentGrades,
    AttendanceSummaryDto AttendanceSummary
);

public record RecentMaterialDto(int Id, string Title, string Type, DateTime CreatedAt);
public record RecentGradeDto(string LessonSubject, double Score, double MaxScore);
public record AttendanceSummaryDto(int TotalAbsences, int TotalLates, int TotalExcused);


// ==========================================
// 2. HANDLER MƏNTİQİ (Müvəqqəti olaraq xətasız struktur)
// ==========================================
public class GetStudentDashboardQueryHandler : IRequestHandler<GetStudentDashboardQuery, StudentDashboardDto>
{
  // Müvəqqəti olaraq verilənlər bazası interfeysini bura bağlamaq üçün konstruktoru boş saxlayırıq.
  // Beləcə paket xətası (EntityFrameworkCore tapılmadı) tamamilə yox olacaq və layihə build olacaq.
  public GetStudentDashboardQueryHandler()
  {
  }

  public async Task<StudentDashboardDto> Handle(GetStudentDashboardQuery request, CancellationToken cancellationToken)
  {
    // Layihənin build olunması və .dll xətalarının itməsi üçün hələlik dummy (boş) data qaytarırıq.
    // Build uğurlu olandan sonra layihənizdəki əsl bazaya keçid interfeysini bura yazacağıq.

    var recentMaterials = new List<RecentMaterialDto>
        {
            new RecentMaterialDto(1, "C# OOP Giriş", "PDF", DateTime.UtcNow)
        };

    var recentGrades = new List<RecentGradeDto>
        {
            new RecentGradeDto("Proqramlaşdırma Təməlləri", 95, 100)
        };

    var summary = new AttendanceSummaryDto(0, 1, 0);

    return await Task.FromResult(new StudentDashboardDto(
        "P324",
        "C# Backend Developer",
        recentMaterials,
        recentGrades,
        summary
    ));
  }
}