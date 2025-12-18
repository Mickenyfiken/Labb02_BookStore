using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {

            builder.HasKey(e => e.Isbn13).HasName("PK__Books__3BF79E03C0B37C10");
            builder.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .HasColumnName("ISBN13");
            builder.Property(e => e.Language).HasMaxLength(20);
            builder.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            builder.Property(e => e.Title).HasMaxLength(50);

            builder.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK_books_publisher");
            builder.HasMany(d => d.Authors).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_bookauthors_authors"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_bookauthors_Books"),
                    j =>
                    {
                        j.HasKey("Isbn13", "AuthorId").HasName("PK__BookAuth__6CFA31C027C12488");
                        j.ToTable("BookAuthors");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .HasColumnName("ISBN13");
                    });

            builder.HasMany(d => d.Categories).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_bookcategories_categories"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_bookcategories_books"),
                    j =>
                    {
                        j.HasKey("Isbn13", "CategoryId").HasName("PK__BookCate__9A670DA177F8F1B6");
                        j.ToTable("BookCategory");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .HasColumnName("ISBN13");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("CategoryID");
                    });
    }
}
