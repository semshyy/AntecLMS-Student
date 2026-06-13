using AntecLMS.Domain.Entities;

namespace AntecLMS.Domain.Repositories;

public interface IAttendanceRepository : IBaseRepository<Attendance>
{
  Task<List<Attendance>> GetByStudentIdAsync(int studentId, CancellationToken ct = default);
}