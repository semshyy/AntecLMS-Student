using AntecLMS.Domain.Common;
using AntecLMS.Domain.Enums;

namespace AntecLMS.Domain.Entities;

public class Teacher : BaseEntity
{
  // Sizin istəyinizə uyğun olaraq UserId int tipindən string tipinə dəyişdirildi
  public int UserId { get; private set; } = default!;
  public string? Specialization { get; private set; }
  public string? Bio { get; private set; }
  public UserStatus Status { get; private set; }

  // Nav (Navigation Properties)
  public User User { get; set; } = default!;
  public ICollection<Group> Groups { get; set; } = new List<Group>();

  protected Teacher() { }

  // Create metodu string userId qəbul edəcək şəkildə yeniləndi
  public static Teacher Create(
    int userId,
    string? specialization,
    string? bio,
    UserStatus status
  ) =>
    new()
    {
      UserId = userId,
      Specialization = specialization,
      Bio = bio,
      Status = status,
    };

  public void Update(string? specialization, string? bio, UserStatus status)
  {
    Specialization = specialization;
    Bio = bio;
    Status = status;
    MarkUpdated();
  }
}