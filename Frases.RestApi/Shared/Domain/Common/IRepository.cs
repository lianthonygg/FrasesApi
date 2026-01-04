namespace FrasesApi.Shared.Domain.Common;

public interface IRepository
{
    // public DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}