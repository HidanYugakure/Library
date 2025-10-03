using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Implementations.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _ctx;
    public AuthorRepository(AppDbContext ctx) => _ctx = ctx;

    public Author Add(Author author)
    {
        _ctx.Authors.Add(author);
        _ctx.SaveChanges();
        return author;
    }
    public List<Author> GetAll() =>
        _ctx.Authors.Include(a => a.Books).ToList();

    public Author? GetById(int id) =>
        _ctx.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
}
