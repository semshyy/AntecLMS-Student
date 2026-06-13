using AntecLMS.Domain.Entities;
using AntecLMS.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AntecLMS.Infrastructure.Persistence.Repositories;

public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
{
  public MaterialRepository(AppDbContext context) : base(context) { }

  public async Task<List<Material>> GetByStudentUserIdAsync(int userId, CancellationToken ct = default) =>
      await _set
          .Include(m => m.Lesson)
          .Where(m => m.IsVisible && m.Lesson.Group.GroupStudents
              .Any(gs => gs.Student.UserId == userId && gs.IsActive))
          .OrderByDescending(m => m.CreatedAt)
          .ToListAsync(ct);
}