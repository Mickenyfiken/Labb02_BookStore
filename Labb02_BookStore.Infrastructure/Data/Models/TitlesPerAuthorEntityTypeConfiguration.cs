using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class TitlesPerAuthorEntityTypeConfiguration : IEntityTypeConfiguration<TitlesPerAuthor>
{
    public void Configure(EntityTypeBuilder<TitlesPerAuthor> builder)
    {

            builder
                .HasNoKey()
                .ToView("TitlesPerAuthor");

            builder.Property(e => e.Name).HasMaxLength(41);
            builder.Property(e => e.TotalValue)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Total Value");

    }
}