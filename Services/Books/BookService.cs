using System.Text.Json;
using SimpleLibrary.Models;

namespace SimpleLibrary.Services.Books;

public class BookService : IBookService
{
    private readonly SimpleLibraryDbContext _context;

    public BookService(SimpleLibraryDbContext context)
    {
        _context = context;
    }

    public List<Book> GetAll()
    {
        return _context.Books.ToList();
    }

    public void Add(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void Update(Book book)
    {
        var existingBook = _context.Books.First(b => b.Id == book.Id);
        existingBook.Name = book.Name;
        existingBook.Description = book.Description;
        existingBook.AuthorId = book.AuthorId;
    }

    public void Delete(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _context.Books.Remove(book);
        }
        _context.SaveChanges();
    }
}