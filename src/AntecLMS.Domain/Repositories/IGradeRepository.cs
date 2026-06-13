using AntecLMS.Domain.Entities;

namespace AntecLMS.Domain.Repositories;

public interface IGradeRepository : IBaseRepository<Grade>
{
  Task<List<Grade>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
}