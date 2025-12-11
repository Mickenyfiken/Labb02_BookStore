using System;
using System.Collections.Generic;

namespace Labb02_BookStore;

public partial class Publisher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Zipcode { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
