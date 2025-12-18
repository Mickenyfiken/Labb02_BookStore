using System;
using System.Collections.Generic;
using Labb02_BookStore.Domain;

namespace Labb02_BookStore.Domain;

public partial class TitlesPerAuthor
{
    public string Name { get; set; } = null!;

    public int? Age { get; set; }

    public int? Titles { get; set; }

    public decimal? TotalValue { get; set; }
}
