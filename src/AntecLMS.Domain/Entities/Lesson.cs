using AntecLMS.Domain.Common;

namespace AntecLMS.Domain.Entities;

public class Lesson : BaseEntity
{
  public string Title { get; private set; } = default!;
  public string? Description { get; private set; }
  public DateOnly LessonDate { get; private set; }
  public int TeacherId { get; private set; } = default!;
  public int GroupId { get; private set; }

  // Nav (Navigation Properties)
  public virtual User Teacher { get; set; } = default!;
  public virtual Group Group { get; set; } = default!;

  public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
  public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
  public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

  protected Lesson() { }

  // Create metodu obyektin təhlükəsiz yaradılmasını təmin edir
  public static Lesson Create(
    string title,
    string? description,
    DateOnly lessonDate,
    int teacherId,
    int groupId
  )
  {
    if (string.IsNullOrWhiteSpace(title))
      throw new ArgumentException("Dərs mövzusu mütləq yazılmalıdır.");

    return new Lesson
    {
      Title = title,
      Description = description,
      LessonDate = lessonDate,
      TeacherId = teacherId,
      GroupId = groupId
    };
  }

  // Müəllimin dərs mövzusunu və ya tarixini redaktə edə bilməsi üçün Update metodu
  public void Update(string title, string? description, DateOnly lessonDate)
  {
    if (string.IsNullOrWhiteSpace(title))
      throw new ArgumentException("Dərs mövzusu mütləq yazılmalıdır.");

    Title = title;
    Description = description;
    LessonDate = lessonDate;
    MarkUpdated();
  }
}