using System;
using System.Collections.Generic;

namespace Labb02_BookStore.Models;

public partial class BookStore
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Street { get; set; }

    public string Zipcode { get; set; } = null!;

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
