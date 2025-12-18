using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

            builder.HasKey(e => e.Id).HasName("PK__Orders__3214EC277D62FBC3");

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.CustomerId).HasMaxLength(13);

            builder.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_customers");

            builder.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orders_bookstores");
    }
}
