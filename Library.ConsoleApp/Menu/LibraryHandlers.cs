using Library.Domain.Entities;
using Library.Domain.Enums;
using static Library.ConsoleApp.Ui;

namespace Library.ConsoleApp.Menu
{
    public static class LibraryHandlers
    {
        public static void ShowBooksMenu()
        {
            while (true)
            {
                Console.Clear();
                Ui.DrawHeader("Books");

                Ui.DrawPanel(new[]
                {
                    "[1] Show All Books",
                    "[2] Get Book By Id",
                    "[3] Create Book",
                    "[4] Delete Book",
                    "",
                    "[0] Back"
                }, width: 56);

                Ui.DrawHint("Choose an option…");
                var k = Console.ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.D1: ListBooks(); break;
                    case ConsoleKey.D2: GetBookById(); break;
                    case ConsoleKey.D3: CreateBook(); break;
                    case ConsoleKey.D4: DeleteBook(); break;
                    case ConsoleKey.D0:
                    case ConsoleKey.Escape: return;
                    default: Ui.Toast("Unknown option", ConsoleColor.Red, 700); break;
                }
            }
        }

        private static void ListBooks()
        {
            var books = Services.Books.GetAll();
            PagedView.ShowPaged(
                title: "Books",
                items: books,
                render: b => $"#{b.Id} {b.Name}  |  {b.PageCount} səh.  |  Author: {b.Author?.Name}"
            );
        }

        private static void GetBookById()
        {
            Console.Write("Book Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Ui.Toast("Invalid Id", ConsoleColor.Red);
                return;
            }

            var b = Services.Books.GetById(id);
            if (b is null)
            {
                Ui.Toast("Not found", ConsoleColor.Red);
                return;
            }

            // Əsas məlumatı bir paneldə göstər
            Ui.PushPage("Book", new[]
            {
        $"#{b.Id} {b.Name}",
        $"Pages: {b.PageCount}",
        $"Author: {b.Author?.Name} {b.Author?.Surname}",
        "",
        "Tarixçə üçün hər hansı düymə bas…"
    });


            var history = (b.ReservedItems ?? new List<ReservedItem>())
                          .OrderByDescending(x => x.StartDate).ToList();

            PagedView.ShowPaged(
                title: $"History: {b.Name}",
                items: history,
                render: r => $"[{r.Id}] UserId:{r.UserId}  {r.StartDate:d} → {r.EndDate:d}  |  {r.Status}"
            );
        }


        private static void CreateBook()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine() ?? "";

            Console.Write("PageCount: ");
            if (!int.TryParse(Console.ReadLine(), out var pc)) { Ui.Toast("Invalid number", ConsoleColor.Red); return; }

            Console.Write("AuthorId: ");
            if (!int.TryParse(Console.ReadLine(), out var aid)) { Ui.Toast("Invalid AuthorId", ConsoleColor.Red); return; }

            if (!Services.Books.AuthorExists(aid)) { Ui.Toast("Author not found", ConsoleColor.Red); return; }

            var book = new Book { Name = name, PageCount = pc, AuthorId = aid };
            book = Services.Books.Add(book);

            Ui.Toast($"Book created #{book.Id} {book.Name}");
        }

        private static void DeleteBook()
        {
            Console.Write("Book Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) { Ui.Toast("Invalid Id", ConsoleColor.Red); return; }

            var ok = Services.Books.Delete(id);
            Ui.Toast(ok ? "Deleted" : "Not found / In use", ok ? ConsoleColor.Green : ConsoleColor.Red);
        }

        public static void ShowAuthorsMenu()
        {
            while (true)
            {
                Console.Clear();
                Ui.DrawHeader("Authors");

                Ui.DrawPanel(new[]
                {
            "[1] Show All Authors",
            "[2] Create Author",
            "[3] Show Author's Books",
            "",
            "[0] Back"
        }, width: 56);

                Ui.DrawHint("Choose an option…");
                var k = Console.ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        ListAuthors();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        CreateAuthor();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        ShowAuthorBooks();
                        break;

                    // 👇 Burada break yox, return olmalıdır!
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.Escape:
                        return;

                    default:
                        Ui.Toast("Unknown option", ConsoleColor.Red, 700);
                        break;
                }
            }
        }

        private static void ListAuthors()
        {
            var authors = Services.Authors.GetAll();
            PagedView.ShowPaged(
                title: "Authors",
                items: authors,
                render: a => $"#{a.Id} {a.Name} {a.Surname}  |  Books: {a.Books.Count}"
            );
        }

        private static void CreateAuthor()
        {
            Console.Write("Name: "); var name = Console.ReadLine() ?? "";
            Console.Write("Surname (optional): "); var sn = Console.ReadLine();
            Console.Write("Gender (0-Female,1-Male,2-Other,3-Unknown): ");
            if (!int.TryParse(Console.ReadLine(), out var gIdx) || gIdx < 0 || gIdx > 3)
            { Ui.Toast("Invalid gender", ConsoleColor.Red); return; }

            var a = new Author
            {
                Name = name,
                Surname = string.IsNullOrWhiteSpace(sn) ? null : sn,
                Gender = (Gender)gIdx
            };

            a = Services.Authors.Add(a);
            Ui.Toast($"Author created #{a.Id} {a.Name}");
        }

        private static void ShowAuthorBooks()
        {
            Console.Write("Author Id: ");
            if (!int.TryParse(Console.ReadLine(), out var id)) { Ui.Toast("Invalid Id", ConsoleColor.Red); return; }

            var a = Services.Authors.GetById(id);
            if (a is null) { Ui.Toast("Author not found", ConsoleColor.Red); return; }

            PagedView.ShowPaged(
                title: $"{a.Name} {a.Surname} – Books",
                items: a.Books.OrderBy(x => x.Name).ToList(),
                render: b => $"#{b.Id} {b.Name}  ({b.PageCount} səh.)"
            );
        }
    }
}
