using System;
using System.Collections.Generic;

namespace Labb02_BookStore.Models;

public partial class SalesPerYear
{
    public string? StoreName { get; set; }

    public int? Year { get; set; }

    public string Title { get; set; } = null!;

    public string? Genre { get; set; }

    public int? BooksSold { get; set; }

    public decimal? AmmountSold { get; set; }
}
