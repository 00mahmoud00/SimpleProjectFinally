using SimpleLibrary.Models;

namespace SimpleLibrary.Services.Authors;

public class AuthorService : IAuthorService
{
    private readonly SimpleLibraryDbContext _context;

    public AuthorService(SimpleLibraryDbContext context)
    {
        _context = context;
    }

    public List<Author> GetAll()
    {
        return _context
            .Authors
            .ToList();
    }

    public void Add(Author author)
    {
        _context.Authors.Add(author);
        _context.SaveChanges();
    }

    public void Update(Author author)
    {
        var existingAuthor = _context.Authors.First(a => a.Id == author.Id);
        existingAuthor.Name = author.Name;
        existingAuthor.Email = author.Email;
        Console.WriteLine(_context.ChangeTracker.Entries().Select(e => e.State).First());
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author != null)
        {
            _context.Authors.Remove(author);
        }
        Console.WriteLine(_context.ChangeTracker.Entries().Select(e => e.State).First());
        _context.SaveChanges();
    }

    public Author GetById(int? bookAuthorId)
    {
        return _context.Authors.First(a => a.Id == bookAuthorId);
    }
}