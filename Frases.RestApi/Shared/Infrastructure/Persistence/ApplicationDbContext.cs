using FrasesApi.Shared.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace FrasesApi.Shared.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IRepository
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // public DbSet<User> Users { get; set; } = null!;
}