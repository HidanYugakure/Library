using Library.Domain.Entities.Common;
using Library.Domain.Enums;

namespace Library.Domain.Entities;
public class ReservedItem : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public Status Status { get; set; }
}
