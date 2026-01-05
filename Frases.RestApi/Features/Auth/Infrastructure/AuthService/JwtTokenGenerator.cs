using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FrasesApi.Features.Auth.Application.Common.Services;
using FrasesApi.Features.Auth.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FrasesApi.Features.Auth.Infrastructure.AuthService;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtSettings) : ITokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)),
            new Claim(ClaimTypes.Role, user.Rol.ToString()),
            new Claim("fullName", user.FullName ?? ""),
            new Claim("nickname", user.Nickname ?? ""),
            new Claim("phone", user.Phone ?? ""),
            new Claim("isAvailable", user.IsAvailable.ToString())
        };

        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }
}