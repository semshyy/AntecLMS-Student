using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AntecLMS.Infrastructure.Services;

public class JwtService : IJwtService
{
  private readonly IConfiguration _config;
  private readonly HashSet<string> _blacklist = new();

  public JwtService(IConfiguration config) => _config = config;

  public string GenerateToken(User user)
  {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new Claim(ClaimTypes.Email, user.Email),
      new Claim(ClaimTypes.Role, user.Role.ToString()),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

    var token = new JwtSecurityToken(
      issuer: _config["Jwt:Issuer"],
      audience: _config["Jwt:Audience"],
      claims: claims,
      expires: DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpiryHours"] ?? "24")),
      signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  public void InvalidateToken(string token) => _blacklist.Add(token);

  public bool IsTokenBlacklisted(string token) => _blacklist.Contains(token);
}
