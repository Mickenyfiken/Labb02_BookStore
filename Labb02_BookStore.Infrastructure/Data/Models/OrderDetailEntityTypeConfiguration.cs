using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {

            builder.HasKey(e => new { e.OrderId, e.Isbn13 }).HasName("PK__OrderDet__E02F222F0F41D5C5");

            builder.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            builder.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            builder.Property(e => e.Quantity).HasDefaultValue(1);

            builder.HasOne(d => d.Isbn13Navigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orderdetails_Books");

            builder.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_orderdetails_orders");
    }
}
