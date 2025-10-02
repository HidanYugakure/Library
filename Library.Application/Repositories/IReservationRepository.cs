using Library.Domain.Entities;
using Library.Domain.Enums;

namespace Library.Application.Interfaces.Repositories;
public interface IReservationRepository
{
    ReservedItem? GetById(int id);
    List<ReservedItem> GetAll();
    List<ReservedItem> GetByUserId(int userId);

    ReservedItem Add(ReservedItem item);
    bool ChangeStatus(int id, Status status);

    int ActiveCountByUser(int userId);
    bool BookExists(int bookId);
    bool IsDateRangeFree(int bookId, DateTime start, DateTime end);
}
