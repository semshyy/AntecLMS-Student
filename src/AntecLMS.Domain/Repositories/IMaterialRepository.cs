using AntecLMS.Domain.Entities;

namespace AntecLMS.Domain.Repositories;

public interface IMaterialRepository : IBaseRepository<Material>
{
  Task<List<Material>> GetByStudentUserIdAsync(int userId, CancellationToken ct = default);
}