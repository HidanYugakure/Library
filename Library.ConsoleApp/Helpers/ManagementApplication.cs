using System.Globalization;

namespace HikariLibrary.ConsoleApp.Helpers;

public class ManagementApplication
{
    private readonly IAuthorService _authorService;
    private readonly IBookService _bookService;
    private readonly IReservedItemService _reservedService;

    public ManagementApplication(IAuthorService a, IBookService b, IReservedItemService r)
    { _authorService = a; _bookService = b; _reservedService = r; }

    public void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Hikari Library (SAFE MODE) ====");
            Console.WriteLine("1. Create Book");
            Console.WriteLine("2. Delete Book");
            Console.WriteLine("3. Get Book by Id");
            Console.WriteLine("4. Show All Books");
            Console.WriteLine("5. Create Author");
            Console.WriteLine("6. Show All Authors");
            Console.WriteLine("7. Show Author's Books");
            Console.WriteLine("8. Reserve Book");
            Console.WriteLine("9. Reservation List");
            Console.WriteLine("10. Change Reservation Status");
            Console.WriteLine("11. User's Reservations List");
            Console.WriteLine("0. Exit");
            Console.Write("Seçim: ");

            var choice = Console.ReadLine();
            if (choice is null) { Pause("Input null gəldi."); continue; }

            try
            {
                switch (choice.Trim())
                {
                    case "1": CreateBook(); break;
                    case "2": DeleteBook(); break;
                    case "3": GetBookById(); break;
                    case "4": ShowAllBooks(); break;
                    case "5": CreateAuthor(); break;
                    case "6": ShowAllAuthors(); break;
                    case "7": ShowAuthorsBooks(); break;
                    case "8": ReserveBook(); break;
                    case "9": ReservationList(); break;
                    case "10": ChangeReservationStatus(); break;
                    case "11": UsersReservations(); break;
                    case "0": return;
                    default: Pause("Yanlış seçim."); break;
                }
            }
            catch (Exception ex) { Pause("Xəta: " + ex); }
        }
    }

    void CreateBook()
    {
        Console.Write("Book Name: "); var name = Console.ReadLine()!;
        Console.Write("PageCount: "); var page = int.Parse(Console.ReadLine()!);
        Console.Write("AuthorId: "); var authorId = int.Parse(Console.ReadLine()!);
        var b = _bookService.Create(name, page, authorId);
        Pause($"Yaradıldı: {b}");
    }
    void DeleteBook()
    {
        Console.Write("Book Id: "); var id = int.Parse(Console.ReadLine()!);
        var ok = _bookServic_
