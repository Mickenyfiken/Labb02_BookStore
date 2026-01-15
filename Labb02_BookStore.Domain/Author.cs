using System;
using System.Collections.Generic;

namespace Labb02_BookStore.Domain;

public partial class Author
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }
    public string FullName => $"{Firstname} {Lastname}";
    public DateOnly? DateOfBirth { get; set; }

    public DateOnly? DateOfDeath { get; set; }

    public virtual ICollection<Book> Isbn13s { get; set; } = new List<Book>();
}
