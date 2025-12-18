using System;
using System.Collections.Generic;


namespace Labb02_BookStore.Domain;

public partial class Category
{
    public int Id { get; set; }

    public string? Genre { get; set; }

    public virtual ICollection<Book> Isbn13s { get; set; } = new List<Book>();
}
