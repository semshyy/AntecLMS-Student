using AntecLMS.Domain.Common;
using AntecLMS.Domain.Enums;

namespace AntecLMS.Domain.Entities;

public class Group : BaseEntity
{
  public string GroupCode { get; private set; } = default!;
  public string Name { get; private set; } = default!;

  public int CourseId { get; private set; }

  // DÜZƏLİŞ: Teacher-ın UserId-si string olduğu üçün bu mütləq string olmalıdır!
  public int TeacherId { get; private set; } = default!;

  public DateOnly StartDate { get; private set; }
  public DateOnly? EndDate { get; private set; }
  public GroupStatus Status { get; private set; }

  // Nav
  public Course Course { get; set; } = default!;
  public Teacher Teacher { get; set; } = default!;
  public ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();

  protected Group() { }

  // DÜZƏLİŞ: teacherId parametri string qəbul edir
  public static Group Create(
    string groupCode,
    string name,
    int courseId,
    int teacherId,
    DateOnly startDate,
    DateOnly? endDate,
    GroupStatus status
  ) =>
    new()
    {
      GroupCode = groupCode,
      Name = name,
      CourseId = courseId,
      TeacherId = teacherId,
      StartDate = startDate,
      EndDate = endDate,
      Status = status,
    };

  // DÜZƏLİŞ: Update metodunda da teacherId string olaraq yeniləndi
  public void Update(string groupCode, string name, int teacherId, GroupStatus status)
  {
    GroupCode = groupCode;
    Name = name;
    TeacherId = teacherId;
    Status = status;
    MarkUpdated();
  }
}