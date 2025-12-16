using System;
using System.Collections.Generic;

namespace Labb02_BookStore.Models;

public partial class Inventory
{
    public int StoreId { get; set; }

    public string Isbn13 { get; set; } = null!;

    public int? Balance { get; set; }

    public virtual Book Isbn13Navigation { get; set; } = null!;

    public virtual BookStore Store { get; set; } = null!;
}
