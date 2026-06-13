using AntecLMS.Domain.Common;
using AntecLMS.Domain.Enums;

namespace AntecLMS.Domain.Entities;

public class Course : BaseEntity
{
  public string Name { get; private set; } = default!;
  public string? Description { get; private set; }
  public CourseStatus Status { get; private set; }

  public DateTime StartDate { get; private set; }
  public DateTime EndDate { get; private set; }

  // Nav
  public ICollection<Group> Groups { get; set; } = new List<Group>();

  protected Course() { }

  public static Course Create(string name, string? description, CourseStatus status, DateTime startDate, DateTime endDate) =>
    new()
    {
      Name = name,
      Description = description,
      Status = status,
      StartDate = startDate,
      EndDate = endDate
    };

  public void Update(string name, string? description, CourseStatus status, DateTime startDate, DateTime endDate)
  {
    Name = name;
    Description = description;
    Status = status;
    StartDate = startDate;
    EndDate = endDate;
    MarkUpdated();
  }
}