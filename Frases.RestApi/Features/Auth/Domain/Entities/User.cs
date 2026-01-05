using FrasesApi.Shared.Domain.Common;
using FrasesApi.Shared.Domain.Constants;

namespace FrasesApi.Features.Auth.Domain.Entities;

public class User: AggregateRoot<Guid>
{
    public User(): base(Guid.NewGuid()){}
    
    public User(Guid id): base(id){}
    
    public required string FullName { get; set; }             
    public required string Nickname { get; set; }               
    public required string Email { get; set; }                 
    public required string Password { get; set; }            
    
    public string? Phone { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public required Roles Rol { get; set; }
}