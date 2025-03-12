using SimpleLibrary.Models;

namespace SimpleLibrary.Services.Books;

public interface IBookService
{
    List<Book> GetAll();
    void Add(Book book);
    void Update(Book book);
    void Delete(int id);
}