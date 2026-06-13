using AntecLMS.Domain.Common;

using AntecLMS.Enums;

namespace AntecLMS.Domain.Entities;

public class Material : BaseEntity
{
  public int LessonId { get; private set; }
  public int TeacherId { get; private set; } = default!;
  public string FileName { get; private set; } = default!;
  public string OriginalFileName { get; private set; } = default!;
  public string FilePath { get; private set; } = default!;
  public string ContentType { get; private set; } = default!;
  public long FileSize { get; private set; }
  public MaterialType Type { get; private set; }
  public bool IsVisible { get; private set; }

  public string? Description { get; private set; }

  // Nav (Navigation Properties)
  public virtual Lesson Lesson { get; set; } = default!;
  public virtual User Teacher { get; set; } = default!;

  protected Material() { }

  // Create metodu fayl məlumatlarının təhlükəsiz şəkildə bazaya yazılmasını təmin edir
  public static Material Create(
    int lessonId,
    int teacherId,
    string fileName,
    string originalFileName,
    string filePath,
    string contentType,
    long fileSize,
    MaterialType type,
    string? description = null,
    bool isVisible = true
  )
  {
    if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(filePath))
      throw new ArgumentException("Fayl adı və yolu boş buraxıla bilməz.");

    if (fileSize <= 0)
      throw new ArgumentException("Fayl ölçüsü sıfırdan böyük olmalıdır.");

    return new Material
    {
      LessonId = lessonId,
      TeacherId = teacherId,
      FileName = fileName,
      OriginalFileName = originalFileName,
      FilePath = filePath,
      ContentType = contentType,
      FileSize = fileSize,
      Type = type,
      Description = description,
      IsVisible = isVisible
    };
  }

  // Materialın gizlədilməsi/göstərilməsi və ya qeydin redaktəsi üçün Update metodu
  public void Update(bool isVisible, string? description)
  {
    IsVisible = isVisible;
    Description = description;
    MarkUpdated();
  }
}