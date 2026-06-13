using AntecLMS.Domain.Entities;

namespace AntecLMS.Domain.Repositories;

public interface ILessonRepository : IBaseRepository<Lesson>
{
  Task<List<Lesson>> GetByStudentUserIdAsync(int userId, CancellationToken ct = default);
}