using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {

            builder.HasKey(e => e.Id).HasName("PK__Categori__3214EC275383ACC8");
            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Genre).HasMaxLength(50);
    }
}
