using Library.Domain.Entities.Common;

namespace Library.Domain.Entities;
public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FinCode { get; set; } = null!;
    public string Password { get; set; } = null!;

    public List<ReservedItem> Reservations { get; set; } = new();
}
