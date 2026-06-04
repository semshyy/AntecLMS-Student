namespace AntecLMS.Application.Common.Exceptions;

public class NotFoundException : Exception
{
  public NotFoundException(string name, int id)
    : base($"'{name}' ({id}) tapılmadı.") { }
}
