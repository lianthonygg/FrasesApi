using FrasesApi.Features.Auth.Application.Common.Services;
using FrasesApi.Features.Auth.Domain.Entities;
using FrasesApi.Shared.Application.Common.Abstractions;
using FrasesApi.Shared.Application.Common.ResultsHandler;
using FrasesApi.Shared.Domain.Common;

namespace FrasesApi.Features.Auth.Application.Commands.Create;

internal sealed class CreateUserCommandHandler(IRepository repository, ITokenGenerator tokenGenerator, IPasswordHasher hasher)
    : ICommandHandler<CreateUserCommand, CreateUserResponseDto>
{
    public async Task<Result<CreateUserResponseDto>> Handle(CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            FullName = request.FullName,
            Nickname = request.Nickname,
            Email = request.Email,
            Phone = request.Phone,
            Password = hasher.Hash(request.Password),
            Rol = request.Rol,
            IsAvailable = true
        };

        repository.Users.Add(user);
        await repository.SaveChangesAsync(cancellationToken);

        // Generate JWT token after successful registration
        var token = tokenGenerator.GenerateToken(user);

        return await Task.FromResult(Result.Success(new CreateUserResponseDto(user.Id, user.Nickname, token, user.Rol)));
    }
}