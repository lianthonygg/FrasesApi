using FrasesApi.Features.Frases.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FrasesApi.Features.Frases.Infrastructure.Persistence.EntityConfiguration;

public class UserEntityConfiguration : IEntityTypeConfiguration<Frase>
{
    public void Configure(EntityTypeBuilder<Frase> builder)
    {
        builder.ToTable("Frases");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnType("uuid")
            .HasConversion<string>();

        builder.Property(u => u.Description)
            .IsRequired();

        builder.Property(u => u.Author)
            .IsRequired();
    }
}