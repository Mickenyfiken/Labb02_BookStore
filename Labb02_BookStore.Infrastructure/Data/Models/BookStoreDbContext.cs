using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Labb02_BookStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Labb02_BookStore.Infrastructure.Data.Model;

public partial class BookStoreDbContext : DbContext
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookStore> BookStores { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<SalesPerYear> SalesPerYears { get; set; }

    public virtual DbSet<TitlesPerAuthor> TitlesPerAuthors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //var config = new ConfigurationBuilder().AddUserSecrets<BookStoreDbContext>().Build();
        //var connectionString = config["ConnectionString"];

        //optionsBuilder.UseSqlServer(connectionString);

        optionsBuilder.UseSqlServer("Data Source=localhost;Database=BookStoreDB;Integrated Security=True;TrustServerCertificate=True;");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        new AuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<Author>());
        new BookEntityTypeConfiguration().Configure(modelBuilder.Entity<Book>());
        new BookStoreEntityTypeConfiguration().Configure(modelBuilder.Entity<BookStore>());
        new CategoryEntityTypeConfiguration().Configure(modelBuilder.Entity<Category>());
        new CustomerEntityTypeConfiguration().Configure(modelBuilder.Entity<Customer>());
        new InventoryEntityTypeConfiguration().Configure(modelBuilder.Entity<Inventory>());
        new OrderEntityTypeConfiguration().Configure(modelBuilder.Entity<Order>());
        new OrderDetailEntityTypeConfiguration().Configure(modelBuilder.Entity<OrderDetail>());
        new PublisherEntityTypeConfiguration().Configure(modelBuilder.Entity<Publisher>());
        new SalesPerYearEntityTypeConfiguration().Configure(modelBuilder.Entity<SalesPerYear>());
        new TitlesPerAuthorEntityTypeConfiguration().Configure(modelBuilder.Entity<TitlesPerAuthor>());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
