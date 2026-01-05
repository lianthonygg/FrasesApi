using FrasesApi.Features.Auth.Domain.Entities;

namespace FrasesApi.Features.Auth.Application.Common.Services;

public interface ITokenGenerator
{
    public string GenerateToken(User user);
}