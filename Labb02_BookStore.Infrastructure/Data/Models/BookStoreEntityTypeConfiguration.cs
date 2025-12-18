using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class BookStoreEntityTypeConfiguration : IEntityTypeConfiguration<BookStore>
{
    public void Configure(EntityTypeBuilder<BookStore> builder)
    {

            builder.HasKey(e => e.Id).HasName("PK__BookStor__3214EC07F16E6E8F");
            builder.Property(e => e.City).HasMaxLength(25);
            builder.Property(e => e.Country).HasMaxLength(25);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(e => e.Street).HasMaxLength(50);
            builder.Property(e => e.Zipcode).HasMaxLength(10);
    }
}
