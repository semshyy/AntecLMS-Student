using AntecLMS.Domain.Entities;

namespace AntecLMS.Application.Common.Interfaces;

public interface IJwtService
{
  string GenerateToken(User user);
  void InvalidateToken(string token);
  bool IsTokenBlacklisted(string token);
}
