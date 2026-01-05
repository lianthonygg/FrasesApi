using FrasesApi.Shared.Domain.Common;

namespace FrasesApi.Features.Frases.Domain.Entities;

public class Frase: AggregateRoot<Guid>
{
    public Frase(): base(Guid.NewGuid()){}
    
    public Frase(Guid id): base(id){}
    
    public required string Description { get; set; }             
    public required string Author { get; set; }   
}