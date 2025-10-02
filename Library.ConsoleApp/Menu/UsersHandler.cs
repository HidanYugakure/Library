using Library.Domain.Entities;
using Library.Domain.Enums;
using static Library.ConsoleApp.Ui;

namespace Library.ConsoleApp.Menu
{
    public static class UsersHandler
    {
        public static void ShowUsersMenu()
        {
            while (true)
            {
                Console.Clear();
                Ui.DrawHeader("Users");

                Ui.DrawPanel(new[]
                {
                    "[1] Show All Users",
                    "[2] Create User",
                    "[3] User Reservations",
                    "[4] Login (FinCode + Password)",
                    "",
                    "[0] Back"
                }, width: 56);

                Ui.DrawHint("Choose an option…");
                var k = Console.ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.D1: ListUsers(); break;
                    case ConsoleKey.D2: CreateUser(); break;
                    case ConsoleKey.D3: ShowUserReservations(); break;
                    case ConsoleKey.D4: Login(); break;
                    case ConsoleKey.D0:
                    case ConsoleKey.Escape: return;
                    default: Ui.Toast("Unknown option", ConsoleColor.Red, 700); break;
                }
            }
        }

        private static void ListUsers()
        {
            var users = Services.Users.GetAll();
            PagedView.ShowPaged(
                title: "Users",
                items: users,
                render: u => $"#{u.Id} {u.FirstName} {u.LastName}  |  FIN: {u.FinCode}"
            );
        }
        private static void CreateUser()
        {
            Console.Write("FirstName: "); var fn = Console.ReadLine() ?? "";
            Console.Write("LastName: "); var ln = Console.ReadLine() ?? "";
            Console.Write("FinCode: "); var fin = Console.ReadLine() ?? "";
            Console.Write("Password: "); var pw = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(fn) || string.IsNullOrWhiteSpace(ln) ||
                string.IsNullOrWhiteSpace(fin) || string.IsNullOrWhiteSpace(pw))
            { Ui.Toast("Fields can't be empty", ConsoleColor.Red); return; }

            var user = new User { FirstName = fn, LastName = ln, FinCode = fin, Password = pw };
            user = Services.Users.Add(user);

            Ui.Toast($"User created: #{user.Id} {user.FirstName} {user.LastName}");
        }
        private static void ShowUserReservations()
        {
            Console.Write("User Id: ");
            if (!int.TryParse(Console.ReadLine(), out var uid)) { Ui.Toast("Invalid Id", ConsoleColor.Red); return; }

            var user = Services.Users.GetById(uid);
            if (user is null) { Ui.Toast("User not found", ConsoleColor.Red); return; }

            var list = (user.Reservations ?? new List<ReservedItem>())
                       .OrderByDescending(x => x.StartDate).ToList();

            PagedView.ShowPaged(
                title: $"Reservations of {user.FirstName} {user.LastName}",
                items: list,
                render: r => $"[{r.Id}] {r.Status,-9}  |  BookId:{r.BookId}  |  {r.StartDate:d}→{r.EndDate:d}"
            );
        }
        private static void Login()
        {
            Console.Write("FinCode: "); var fin = Console.ReadLine() ?? "";
            Console.Write("Password: "); var pw = Console.ReadLine() ?? "";

            var user = Services.Users.Authenticate(fin, pw);
            Ui.Toast(user is null ? "Login failed!" : $"Welcome {user.FirstName} {user.LastName}",
                user is null ? ConsoleColor.Red : ConsoleColor.Green);
        }

        public static void ShowReservationsMenu()
        {
            while (true)
            {
                Console.Clear();
                Ui.DrawHeader("Reservations");

                Ui.DrawPanel(new[]
                {
                    "[1] List All (grouped by Status)",
                    "[2] Create Reservation",
                    "[3] Change Reservation Status",
                    "[4] User's Reservations by FinCode (optional filter)",
                    "",
                    "[0] Back"
                }, width: 56);

                Ui.DrawHint("Choose an option…");
                var k = Console.ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.D1: ListReservations(); break;
                    case ConsoleKey.D2: CreateReservation(); break;
                    case ConsoleKey.D3: ChangeReservationStatus(); break;
                    case ConsoleKey.D4: ListUserReservationsByFin(); break;
                    case ConsoleKey.D0:
                    case ConsoleKey.Escape: return;
                    default: Ui.Toast("Unknown option", ConsoleColor.Red, 700); break;
                }
            }
        }
        private static void ListReservations()
        {
            var items = Services.Reservations.GetAll()
                .OrderBy(r => r.Status)
                .ThenByDescending(r => r.Id)
                .ToList();

            PagedView.ShowPaged(
                title: "Reservations (by Status)",
                items: items,
                render: r => $"[{r.Id}] {r.Status,-9}  |  UserId:{r.UserId}  BookId:{r.BookId}  {r.StartDate:d}→{r.EndDate:d}"
            );
        }
        private static void CreateReservation()
        {
            Console.Write("Book Id: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId)) { Ui.Toast("Invalid BookId", ConsoleColor.Red); return; }

            if (!Services.Reservations.BookExists(bookId)) { Ui.Toast("Book not found", ConsoleColor.Red); return; }

            Console.Write("User Id: ");
            if (!int.TryParse(Console.ReadLine(), out var userId)) { Ui.Toast("Invalid UserId", ConsoleColor.Red); return; }

            var user = Services.Users.GetById(userId);
            if (user is null) { Ui.Toast("User not found", ConsoleColor.Red); return; }

            Console.Write("Start (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var s)) { Ui.Toast("Invalid date", ConsoleColor.Red); return; }

            Console.Write("End   (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var e)) { Ui.Toast("Invalid date", ConsoleColor.Red); return; }

            if (e <= s) { Ui.Toast("End must be after Start", ConsoleColor.Red); return; }

            var active = Services.Reservations.ActiveCountByUser(userId);
            if (active >= 3) { Ui.Toast("This user already has 3 active books", ConsoleColor.Red); return; }

            var free = Services.Reservations.IsDateRangeFree(bookId, s, e);
            if (!free) { Ui.Toast("Book is busy in that range", ConsoleColor.Red); return; }

            var item = new ReservedItem
            {
                BookId = bookId,
                UserId = userId,
                StartDate = s,
                EndDate = e,
                Status = Status.Confirmed
            };

            item = Services.Reservations.Add(item);
            Ui.Toast($"Reserved: [{item.Id}] User:{userId} Book:{bookId}");
        }

        private static void ChangeReservationStatus()
        {
            Console.Write("Reservation Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) { Ui.Toast("Invalid Id", ConsoleColor.Red); return; }

            Console.Write("Status (0-Confirmed,1-Started,2-Completed,3-Canceled): ");
            if (!int.TryParse(Console.ReadLine(), out var stVal) || stVal < 0 || stVal > 3)
            { Ui.Toast("Invalid status", ConsoleColor.Red); return; }

            var ok = Services.Reservations.ChangeStatus(id, (Status)stVal);
            Ui.Toast(ok ? "Status changed" : "Not found", ok ? ConsoleColor.Green : ConsoleColor.Red);
        }

        private static void ListUserReservationsByFin()
        {
            Console.Write("FinCode: ");
            var fin = Console.ReadLine() ?? "";

            var user = Services.Users.GetAll()
                .FirstOrDefault(u => string.Equals(u.FinCode, fin, StringComparison.OrdinalIgnoreCase));

            if (user is null) { Ui.Toast("User not found", ConsoleColor.Red); return; }

            var list = Services.Reservations.GetByUserId(user.Id)
                .OrderByDescending(x => x.StartDate).ToList();

            PagedView.ShowPaged(
                title: $"Reservations of {user.FirstName} {user.LastName} (FIN {user.FinCode})",
                items: list,
                render: r => $"[{r.Id}] {r.Status,-9}  |  BookId:{r.BookId}  |  {r.StartDate:d}→{r.EndDate:d}"
            );
        }

    }
}
