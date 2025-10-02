using Library.Domain.Entities.Common;
using Library.Domain.Enums;

namespace Library.Domain.Entities;
public class Author : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Surname { get; set; }
    public Gender Gender { get; set; }
    public List<Book> Books { get; set; } = new();
}