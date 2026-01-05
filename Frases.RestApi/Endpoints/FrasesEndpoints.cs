using FrasesApi.Data.Frase;
using FrasesApi.Features.Frases.Application.Commands.Create;
using FrasesApi.Features.Frases.Application.Queries.GetAll;
using FrasesApi.Shared.Application.Common.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrasesApi.Endpoints;

public static class FrasesEndpoints
{
    public static void MapFrasesEndpoints(this WebApplication app)
    {
        var frasesGroup = app
            .MapGroup("/frases")
            .WithTags("Frases")
            .WithOpenApi();

        frasesGroup.MapGet("/",
                async ([FromServices] IQueryHandler<GetAllFrasesQuery, List<GetAllFrasesResponseDto>> handler) =>
                {
                    var result = await handler.Handle(new GetAllFrasesQuery());

                    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
                })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "FindAll Frases",
                Description = "FindAll Frases in the system",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Frases" } }
            });

        frasesGroup.MapPost("/",
                async ([FromBody] CreateFraseDto dto,
                    [FromServices] ICommandHandler<CreateFraseCommand, CreateFraseResponseDto> handler) =>
                {
                    var command = dto.MapToCommand();
                    var result = await handler.Handle(command);

                    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
                })
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Creates a frase",
                Description = "Creates a frase in the system",
                Tags = new List<Microsoft.OpenApi.Models.OpenApiTag> { new() { Name = "Frases" } }
            }).RequireAuthorization("Admin");
    } 
}