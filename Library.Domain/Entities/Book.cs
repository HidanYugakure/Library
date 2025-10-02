using Library.Domain.Entities.Common;

namespace Library.Domain.Entities;
public class Book : BaseEntity
{
    public string Name { get; set; } = null!;
    public int PageCount { get; set; }

    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    public List<ReservedItem> ReservedItems { get; set; } = new();
}