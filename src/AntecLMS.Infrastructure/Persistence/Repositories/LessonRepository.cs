using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntecLMS.Infrastructure.Persistence.Repositories;

public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
{
  public LessonRepository(AppDbContext context) : base(context) { }

  public async Task<List<Lesson>> GetByStudentUserIdAsync(int userId, CancellationToken ct = default) =>
      await _set
          .Include(l => l.Group)
          .Include(l => l.Materials.Where(m => m.IsVisible))
          .Where(l => l.Group.GroupStudents.Any(gs => gs.Student.UserId == userId && gs.IsActive))
          .OrderByDescending(l => l.LessonDate)
          .ToListAsync(ct);
}