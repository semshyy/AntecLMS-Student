using System;

namespace AntecLMS.Domain.Entities;

public class GroupStudent
{
  public int GroupId { get; set; }

  // Sizin istəyinizə uyğun olaraq string tipinə dəyişdirildi
  public int StudentId { get; set; } = default!;

  public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
  public bool IsActive { get; set; } = true;

  // Nav (Navigation Properties)
  public Group Group { get; set; } = default!;
  public Student Student { get; set; } = default!;
}