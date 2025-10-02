using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.Implementations.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _ctx;
    public ReservationRepository(AppDbContext ctx) => _ctx = ctx;

    public ReservedItem Add(ReservedItem item)
    {
        // əlaqələr
        var bookExists = _ctx.Books.Any(b => b.Id == item.BookId);
        if (!bookExists) throw new InvalidOperationException("Book not found");

        var userExists = _ctx.Users.Any(u => u.Id == item.UserId);
        if (!userExists) throw new InvalidOperationException("User not found");

        // tarix overlap yoxlaması (LINQ to Entities)
        if (!IsDateRangeFree(item.BookId, item.StartDate, item.EndDate))
            throw new InvalidOperationException("Book is busy in that date range");

        // eyni anda max 3 aktiv
        if (ActiveCountByUser(item.UserId) >= 3)
            throw new InvalidOperationException("User already has 3 active reservations");

        _ctx.ReservedItems.Add(item);
        _ctx.SaveChanges();

        return _ctx.ReservedItems
            .Include(r => r.Book)
            .Include(r => r.User)
            .First(r => r.Id == item.Id);
    }

    public bool ChangeStatus(int id, Status status)
    {
        var r = _ctx.ReservedItems.FirstOrDefault(x => x.Id == id);
        if (r is null) return false;
        r.Status = status;
        _ctx.SaveChanges();
        return true;
    }

    public List<ReservedItem> GetAll() =>
        _ctx.ReservedItems
            .Include(r => r.Book)
            .Include(r => r.User)
            .OrderBy(r => r.Status)
            .ThenByDescending(r => r.Id)
            .ToList();

    public ReservedItem? GetById(int id) =>
        _ctx.ReservedItems
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefault(r => r.Id == id);

    public List<ReservedItem> GetByUserId(int userId) =>
        _ctx.ReservedItems
            .Include(r => r.Book)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.StartDate)
            .ToList();

    public int ActiveCountByUser(int userId) =>
        _ctx.ReservedItems.Count(r => r.UserId == userId &&
            (r.Status == Status.Confirmed || r.Status == Status.Started));

    public bool BookExists(int bookId) =>
        _ctx.Books.Any(b => b.Id == bookId);

    public bool IsDateRangeFree(int bookId, DateTime start, DateTime end)
    {
        // kəsişmə yoxdur: end <= s || start >= e
        return !_ctx.ReservedItems.Any(r =>
            r.BookId == bookId &&
            r.Status != Status.Canceled &&
            r.Status != Status.Completed &&
            !(end <= r.StartDate || start >= r.EndDate));
    }
}
