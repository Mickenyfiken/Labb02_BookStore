using System;
using System.Collections.Generic;


namespace Labb02_BookStore.Domain;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Zipcode { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
