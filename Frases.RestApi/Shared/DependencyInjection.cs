using FluentValidation;
using FrasesApi.Shared.Application;
using FrasesApi.Shared.Application.Common.Abstractions;
using FrasesApi.Shared.Application.Common.Behaviours;
using FrasesApi.Shared.Domain.Common;
using FrasesApi.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FrasesApi.Shared;

public static class DependencyInjection
{
    public static void AddShared(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository(configuration);
        services.AddApplicationRepository();
    }

   private static void AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrEmpty(connectionString))
        throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");


    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));


    // Register IRepository as the DbContext
    services.AddScoped<IRepository>(provider => provider.GetRequiredService<ApplicationDbContext>());
    }
   
    private static void AddApplicationRepository(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyMarker>();
        
        services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        // Register decorators for validation and logging
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
        services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationDecorator.QueryHandler<,>));

        services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
    }
}