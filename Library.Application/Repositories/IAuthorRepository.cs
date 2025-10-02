using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories;
public interface IAuthorRepository
{
    Author? GetById(int id);
    List<Author> GetAll();
    Author Add(Author author);
}
