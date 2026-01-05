using FrasesApi.Features.Auth.Domain.Entities;
using FrasesApi.Features.Frases.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrasesApi.Shared.Domain.Common;

public interface IRepository
{
    public DbSet<User> Users { get; set; }
    public DbSet<Frase> Frases { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}