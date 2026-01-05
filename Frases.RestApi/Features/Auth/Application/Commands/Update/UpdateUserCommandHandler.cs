using FrasesApi.Shared.Application.Common.Abstractions;
using FrasesApi.Shared.Application.Common.ResultsHandler;
using FrasesApi.Shared.Domain.Common;

namespace FrasesApi.Features.Auth.Application.Commands.Update;

internal sealed class UpdateUserCommandHandler(IRepository repository)
    : ICommandHandler<UpdateUserCommand, UpdateUserResponseDto>
{
    public async Task<Result<UpdateUserResponseDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.Users.FindAsync(request.Id);
        if (user is null)
        {
            throw new Exception("User not found");
        }

        user.FullName = request.FullName;
        user.Nickname = request.NickName;
        user.Email = request.Email;
        user.Phone = request.Phone;
        
        await repository.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(Result.Success(new UpdateUserResponseDto(user.Id)));
    }
}