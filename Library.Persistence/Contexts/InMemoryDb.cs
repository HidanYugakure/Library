//using Library.Domain.Entities;
//using Library.Domain.Enums;

//namespace Library.Persistence.Contexts;

//public static class InMemoryDb
//{
//    public static readonly List<Author> Authors = new();
//    public static readonly List<Book> Books = new();
//    public static readonly List<User> Users = new();
//    public static readonly List<ReservedItem> Reservations = new();

//    private static int _id = 1;
//    public static int NextId() => _id++;

//    static InMemoryDb()
//    {

//        var a1 = new Author { Id = NextId(), Name = "Jules", Surname = "Verne", Gender = Gender.Male };
//        var a2 = new Author { Id = NextId(), Name = "Agatha", Surname = "Christie", Gender = Gender.Female };
//        Authors.AddRange(new[] { a1, a2 });

//        var b1 = new Book { Id = NextId(), Name = "Twenty Thousand Leagues", PageCount = 360, AuthorId = a1.Id, Author = a1 };
//        var b2 = new Book { Id = NextId(), Name = "Murder on the Orient Express", PageCount = 280, AuthorId = a2.Id, Author = a2 };
//        Books.AddRange(new[] { b1, b2 });
//        a1.Books.Add(b1); a2.Books.Add(b2);

//        var u1 = new User { Id = NextId(), FirstName = "Ali", LastName = "Mammadov", FinCode = "AAA111", Password = "1234" };
//        var u2 = new User { Id = NextId(), FirstName = "Leyla", LastName = "Huseynova", FinCode = "BBB222", Password = "1234" };
//        Users.AddRange(new[] { u1, u2 });
//    }
//}
