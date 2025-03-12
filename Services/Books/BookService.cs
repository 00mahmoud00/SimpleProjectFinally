using System.Text.Json;
using SimpleLibrary.Models;

namespace SimpleLibrary.Services.Books;

public class BookService : IBookService
{
    private static List<Book> books = new List<Book>();
    
    public List<Book> GetAll()
    {
        return books;
    }

    public void Add(Book book)
    {
        book.Id = books.Count + 1;
        books.Add(book);
        Console.WriteLine(JsonSerializer.Serialize(book));
    }

    public void Update(Book book)
    {
        var existingBook = books.First(b => b.Id == book.Id);
        existingBook.Name = book.Name;
        existingBook.Description = book.Description;
        existingBook.AuthorId = book.AuthorId;
    }

    public void Delete(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            books.Remove(book);
        }
    }
}