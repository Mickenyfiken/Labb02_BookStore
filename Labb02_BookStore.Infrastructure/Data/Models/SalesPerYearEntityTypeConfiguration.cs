using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class SalesPerYearEntityTypeConfiguration : IEntityTypeConfiguration<SalesPerYear>
{
    public void Configure(EntityTypeBuilder<SalesPerYear> builder)
    {
            builder
                .HasNoKey()
                .ToView("SalesPerYear");

            builder.Property(e => e.AmmountSold).HasColumnType("decimal(38, 2)");
            builder.Property(e => e.BooksSold).HasColumnName("Books Sold");
            builder.Property(e => e.Genre)
                .HasMaxLength(50)
                .HasColumnName("genre");
            builder.Property(e => e.StoreName).HasMaxLength(50);
            builder.Property(e => e.Title).HasMaxLength(50);
    }
}
