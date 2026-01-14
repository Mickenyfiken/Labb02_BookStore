using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace Labb02_BookStore.Domain;

public partial class Book
{
    public string Isbn13 { get; set; } = null!;
    
    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public decimal? Price { get; set; }

    public DateTime? DateOfIssue { get; set; }

    public int? PublisherId { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
