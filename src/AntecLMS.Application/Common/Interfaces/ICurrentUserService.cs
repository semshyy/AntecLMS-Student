using AntecLMS.Domain.Enums;

namespace AntecLMS.Application.Common.Interfaces;

public interface ICurrentUserService
{
  int UserId { get; }
  UserRole Role { get; }
  string Email { get; }
}
