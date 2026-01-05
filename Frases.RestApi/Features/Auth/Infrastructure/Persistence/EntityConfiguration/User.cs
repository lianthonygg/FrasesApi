using FrasesApi.Features.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrasesApi.Features.Auth.Infrastructure.Persistence.EntityConfiguration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasConversion<string>();

        builder.Property(u => u.FullName)
            .IsRequired();

        builder.Property(u => u.Nickname)
            .IsRequired();

        builder.Property(u => u.Phone)
            .IsRequired();

        builder.Property(u => u.Email)
            .IsRequired();

        builder.Property(u => u.Password)
            .IsRequired();

        builder.Property(u => u.Rol);

        builder.Property(u => u.IsAvailable)
            .HasColumnType("boolean")
            .HasDefaultValue(true);
    }
}