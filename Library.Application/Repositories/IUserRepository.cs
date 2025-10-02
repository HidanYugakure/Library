using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;
public interface IUserRepository
{
    User? GetById(int id);
    List<User> GetAll();
    User Add(User user);
    User? Authenticate(string finCode, string password);
}
