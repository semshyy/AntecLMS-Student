using AntecLMS.Domain.Common;

namespace AntecLMS.Domain.Entities;

public class Grade : BaseEntity
{
  public int LessonId { get; private set; }

  // Sizin arxitekturaya tam uyğun olaraq string tipində saxlanıldı
  public int StudentId { get; private set; } = default!;
  public double Score { get; private set; }
  public double MaxScore { get; private set; }
  public string? Comment { get; private set; }

  // Nav (Navigation Properties)
  public virtual Lesson Lesson { get; set; } = default!;

  // Üst-üstə düşmə tam təmin olundu: Birbaşa Student entity-sinə bağlanır
  public virtual Student Student { get; set; } = default!;

  protected Grade() { }

  public static Grade Create(
    int lessonId,
    int studentId,
    double score,
    double maxScore = 100,
    string? comment = null
  )
  {
    // Domain Validation (Biznes Qaydası)
    if (score < 0 || score > maxScore)
      throw new ArgumentException($"Bal 0 ilə maksimum bal ({maxScore}) arasında olmalıdır.");

    return new Grade
    {
      LessonId = lessonId,
      StudentId = studentId,
      Score = score,
      MaxScore = maxScore,
      Comment = comment
    };
  }

  public void Update(double score, double maxScore, string? comment)
  {
    if (score < 0 || score > maxScore)
      throw new ArgumentException($"Bal 0 ilə maksimum bal ({maxScore}) arasında olmalıdır.");

    Score = score;
    MaxScore = maxScore;
    Comment = comment;
    MarkUpdated();
  }
}