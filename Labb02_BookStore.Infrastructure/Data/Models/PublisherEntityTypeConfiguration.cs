using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class PublisherEntityTypeConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {

            builder.HasKey(e => e.Id).HasName("PK__Publishe__3214EC073DF8E4FE");

            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(e => e.City).HasMaxLength(25);
            builder.Property(e => e.Country).HasMaxLength(25);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Street).HasMaxLength(50);
    }
}
