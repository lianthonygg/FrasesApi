using FrasesApi.Features.Auth.Domain.Entities;
using FrasesApi.Features.Frases.Domain.Entities;
using FrasesApi.Shared.Domain.Common;
using FrasesApi.Shared.Domain.Constants;
using FrasesApi.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrasesApi.Shared.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IRepository
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {}

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Frase> Frases { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<RolClass>().HasData(
            Enum.GetValues(typeof(Roles)).Cast<Roles>()
                .Select(rol => new RolClass()
                    {
                        Rol = rol,
                        Id = rol.ToString()
                    }
                ));
    }
}