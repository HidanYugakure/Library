using Library.Application.Interfaces.Repositories;

namespace Library.ConsoleApp;

internal static class Services
{
    public static IAuthorRepository Authors { get; private set; } = default!;
    public static IBookRepository Books { get; private set; } = default!;
    public static IReservationRepository Reservations { get; private set; } = default!;
    public static IUserRepository Users { get; private set; } = default!;
    public static void Init(
        IAuthorRepository a,
        IBookRepository b,
        IReservationRepository r,
        IUserRepository u)
    { Authors = a; Books = b; Reservations = r; Users = u; }
}
