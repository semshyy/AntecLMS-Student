using AntecLMS.Domain.Common;
using AntecLMS.Domain.Enums;

namespace AntecLMS.Domain.Entities;

public class Student : BaseEntity
{
  // Sizin istəyinizə uyğun olaraq UserId string tipinə dəyişdirildi
  public int UserId { get; private set; } = default!;
  public string? Phone { get; private set; } // Köhnə kodunuzdan əlavə edilən xüsusiyyət
  public DateOnly? BirthDate { get; private set; }
  public string? Note { get; private set; }
  public UserStatus Status { get; private set; }

  // Nav
  public User User { get; set; } = default!;
  public ICollection<GroupStudent> GroupStudents { get; set; } = new List<GroupStudent>();

  protected Student() { }

  // Create metodu string userId və phone qəbul edəcək şəkildə yeniləndi
  public static Student Create(int userId, string? phone, DateOnly? birthDate, string? note, UserStatus status) =>
    new()
    {
      UserId = userId,
      Phone = phone,
      BirthDate = birthDate,
      Note = note,
      Status = status,
    };

  // Update metodu phone (telefon) dəyişikliyini dəstəkləyəcək şəkildə tənzimləndi
  public void Update(string? phone, string? note, UserStatus status, DateOnly? birthDate = null)
  {
    Phone = phone;
    Note = note;
    Status = status;
    BirthDate = birthDate ?? BirthDate;
    MarkUpdated();
  }
}