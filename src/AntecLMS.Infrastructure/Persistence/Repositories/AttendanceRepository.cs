using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntecLMS.Infrastructure.Persistence.Repositories;

public class AttendanceRepository : BaseRepository<Attendance>, IAttendanceRepository
{
  public AttendanceRepository(AppDbContext context) : base(context) { }

  public async Task<List<Attendance>> GetByStudentIdAsync(int studentId, CancellationToken ct = default) =>
      await _set
          .Include(a => a.Lesson)
          .Where(a => a.StudentId == studentId)
          .OrderByDescending(a => a.Lesson.LessonDate)
          .ToListAsync(ct);
}