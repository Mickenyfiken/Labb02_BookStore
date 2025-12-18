using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
            builder.HasKey(e => e.Id).HasName("PK__Authors__3214EC07DED022AC");
            builder.Property(e => e.Firstname).HasMaxLength(20);
            builder.Property(e => e.Lastname).HasMaxLength(20);
    }
}
