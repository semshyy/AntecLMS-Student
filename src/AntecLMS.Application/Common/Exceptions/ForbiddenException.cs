namespace AntecLMS.Application.Common.Exceptions;

public class ForbiddenException : Exception
{
  public ForbiddenException()
    : base("Bu əməliyyat üçün icazəniz yoxdur.") { }
}
