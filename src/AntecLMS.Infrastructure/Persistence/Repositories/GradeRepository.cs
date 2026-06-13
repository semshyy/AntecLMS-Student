using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntecLMS.Infrastructure.Persistence.Repositories;

public class GradeRepository : BaseRepository<Grade>, IGradeRepository
{
  public GradeRepository(AppDbContext context) : base(context) { }

  public async Task<List<Grade>> GetByStudentIdAsync(int studentId, CancellationToken ct = default) =>
      await _set
          .Include(g => g.Lesson)
          .Where(g => g.StudentId == studentId)
          .OrderByDescending(g => g.Lesson.LessonDate)
          .ToListAsync(ct);
}