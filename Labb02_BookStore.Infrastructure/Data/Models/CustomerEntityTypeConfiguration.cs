using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {

            builder.HasKey(e => e.Id).HasName("PK__Customer__3214EC073948098B");
            builder.Property(e => e.Id).HasMaxLength(13);
            builder.Property(e => e.Address).HasMaxLength(25);
            builder.Property(e => e.City).HasMaxLength(25);
            builder.Property(e => e.Country).HasMaxLength(25);
            builder.Property(e => e.Firstname).HasMaxLength(25);
            builder.Property(e => e.Lastname).HasMaxLength(25);
            builder.Property(e => e.Zipcode).HasMaxLength(10);
    }
}
