using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Implementations.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _ctx;
    public BookRepository(AppDbContext ctx) => _ctx = ctx;

    public Book Add(Book book)
    {
        // author yoxla
        var authorExists = _ctx.Authors.Any(a => a.Id == book.AuthorId);
        if (!authorExists) throw new InvalidOperationException("Author not found");

        _ctx.Books.Add(book);
        _ctx.SaveChanges();

        // geri dönəndə Author + history ilə
        return _ctx.Books
            .Include(b => b.Author)
            .Include(b => b.ReservedItems)
            .First(b => b.Id == book.Id);
    }

    public bool Delete(int id)
    {
        var b = _ctx.Books.FirstOrDefault(x => x.Id == id);
        if (b is null) return false;

        // optional: aktiv rezerv varsa silmə
        var inUse = _ctx.ReservedItems.Any(r => r.BookId == id &&
            (r.Status == Library.Domain.Enums.Status.Confirmed ||
             r.Status == Library.Domain.Enums.Status.Started));
        if (inUse) return false;

        _ctx.Books.Remove(b);
        _ctx.SaveChanges();
        return true;
    }

    public List<Book> GetAll() =>
        _ctx.Books
            .Include(b => b.Author)
            .ToList();

    public Book? GetById(int id) =>
        _ctx.Books
            .Include(b => b.Author)
            .Include(b => b.ReservedItems)
            .FirstOrDefault(b => b.Id == id);

    public bool AuthorExists(int authorId) =>
        _ctx.Authors.Any(a => a.Id == authorId);
}
