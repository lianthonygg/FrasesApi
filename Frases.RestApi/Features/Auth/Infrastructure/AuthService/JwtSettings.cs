namespace FrasesApi.Features.Auth.Infrastructure.AuthService;

public class JwtSettings
{
    public required string SecretKey { get; set; }
    public string Issuer { get; set; } = "Frases";
    public string Audience { get; set; } = "FrasesBackend";
    public int ExpirationHours { get; set; } = 1;
}