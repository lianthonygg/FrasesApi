using FrasesApi.Features.Auth.Application.Common.Services;
using FrasesApi.Features.Auth.Infrastructure.AuthService;

namespace FrasesApi.Features.Auth;

public static class DependencyInjection
{
    public static void AddAuthModule(this IServiceCollection services)
    {
        services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}