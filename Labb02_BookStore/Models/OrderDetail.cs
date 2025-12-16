using System;
using System.Collections.Generic;

namespace Labb02_BookStore.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public string Isbn13 { get; set; } = null!;

    public decimal? Price { get; set; }

    public int Quantity { get; set; }

    public virtual Book Isbn13Navigation { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
