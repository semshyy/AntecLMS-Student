using AntecLMS.Domain.Common;

using AntecLMS.Enums;

namespace AntecLMS.Domain.Entities;

public class Attendance : BaseEntity
{
  public int LessonId { get; private set; }
  public int StudentId { get; private set; } = default!;
  public AttendanceStatus Status { get; private set; }
  public int? LateMinutes { get; private set; }
  public string? Reason { get; private set; }
  public string? Note { get; private set; }

  // Nav (Navigation Properties)
  public virtual Lesson Lesson { get; set; } = default!;
  public virtual User Student { get; set; } = default!;

  protected Attendance() { }

  // Create metodu obyektin biznes qaydalarına uyğun yaradılmasını təmin edir
  public static Attendance Create(
    int lessonId,
    int studentId,
    AttendanceStatus status,
    int? lateMinutes = null,
    string? reason = null,
    string? note = null
  )
  {
    // Domain Validation: Əgər gecikmə dəqiqəsi mənfi daxil edilərsə xəta versin
    if (lateMinutes.HasValue && lateMinutes.Value < 0)
      throw new ArgumentException("Gecikmə dəqiqəsi mənfi ola bilməz.");

    return new Attendance
    {
      LessonId = lessonId,
      StudentId = studentId,
      Status = status,
      LateMinutes = lateMinutes,
      Reason = reason,
      Note = note
    };
  }

  // Müəllimin sonradan davamiyyət statusunu və ya qeydlərini dəyişə bilməsi üçün Update metodu
  public void Update(AttendanceStatus status, int? lateMinutes, string? reason, string? note)
  {
    if (lateMinutes.HasValue && lateMinutes.Value < 0)
      throw new ArgumentException("Gecikmə dəqiqəsi mənfi ola bilməz.");

    Status = status;
    LateMinutes = lateMinutes;
    Reason = reason;
    Note = note;
    MarkUpdated();
  }
}