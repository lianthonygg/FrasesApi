using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FrasesApi.Shared.Infrastructure.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
  public ApplicationDbContext CreateDbContext(string[] args)
  {
    Env.Load();

    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

    var connectionString = Env.GetString("DATABASE_URL"); 
    optionsBuilder.UseNpgsql(connectionString);

    return new ApplicationDbContext(optionsBuilder.Options);
  }
}