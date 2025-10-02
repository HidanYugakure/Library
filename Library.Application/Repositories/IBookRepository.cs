using System;
using Library.Domain.Entities;
using Library.Domain.Enums;

namespace Library.Application.Interfaces.Repositories;
public interface IBookRepository
{
    Book? GetById(int id);
    List<Book> GetAll();
    Book Add(Book book);
    bool Delete(int id);
    bool AuthorExists(int authorId);
}


