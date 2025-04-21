using System.Text.Json;
using SimpleLibrary.Models;

namespace SimpleLibrary.Services.Books;

public class BookService(SimpleLibraryDbContext context) : IBookService
{
    public List<Book> GetAll()
    {
        return context.Books.ToList();
    }

    public void Add(Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
    }

    public void Update(Book book)
    {
        var existingBook = context.Books.First(b => b.Id == book.Id);
        existingBook.Name = book.Name;
        existingBook.Description = book.Description;
        existingBook.AuthorId = book.AuthorId;
    }

    public void Delete(int id)
    {
        var book = context.Books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            context.Books.Remove(book);
        }
        context.SaveChanges();
    }
}