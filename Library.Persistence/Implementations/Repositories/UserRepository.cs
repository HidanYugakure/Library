using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Implementations.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;
    public UserRepository(AppDbContext ctx) => _ctx = ctx;

    public User Add(User user)
    {
        _ctx.Users.Add(user);
        _ctx.SaveChanges();
        return user;
    }

    public List<User> GetAll() =>
        _ctx.Users.Include(u => u.Reservations).ToList();

    public User? GetById(int id) =>
        _ctx.Users.Include(u => u.Reservations).FirstOrDefault(u => u.Id == id);

    public User? Authenticate(string finCode, string password) =>
        _ctx.Users.FirstOrDefault(u => u.FinCode == finCode && u.Password == password);
}
