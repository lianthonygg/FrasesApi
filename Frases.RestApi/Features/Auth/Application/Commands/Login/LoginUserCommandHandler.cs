using FrasesApi.Features.Auth.Application.Common.Services;
using FrasesApi.Shared.Application.Common.Abstractions;
using FrasesApi.Shared.Application.Common.ResultsHandler;
using FrasesApi.Shared.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace FrasesApi.Features.Auth.Application.Commands.Login;

internal class LoginUserCommandHandler(ITokenGenerator tokenGenerator, IRepository repository, IPasswordHasher hasher)
    : ICommandHandler<LoginUserCommand, LoginUserResponseDto>
{
    public async Task<Result<LoginUserResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        
        var user = await repository.Users.FirstOrDefaultAsync(user => user.Email == request.Email, cancellationToken);
        if (user is null)
        {
            throw new Exception("User not found: " + request.Email);
            // throw new UserNotFoundException("User not found", request.Email);
        }

        if (!hasher.Verify(request.Password, user.Password))
        {
            throw new Exception("Invalid credentials");
            // throw new InvalidCredentialsException("Invalid credentials");
        }

        var token = tokenGenerator.GenerateToken(user);
        return await Task.FromResult(Result.Success(new LoginUserResponseDto(token, user.Id, user.Rol)));
    }
}