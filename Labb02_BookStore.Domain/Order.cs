using System;
using System.Collections.Generic;


namespace Labb02_BookStore.Domain;

public partial class Order
{
    public int Id { get; set; }

    public string CustomerId { get; set; } = null!;

    public int StoreId { get; set; }

    public DateOnly CreatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual BookStore Store { get; set; } = null!;
}
