using SimpleLibrary.Models;

namespace SimpleLibrary.Services.Authors;

public class AuthorService : IAuthorService
{
    private static List<Author> _authors = new List<Author>()
    {
        new Author() { Name = "Mahmoud", Email = "mahmoud@gmail.com", Id = 1 },
    };

    public List<Author> GetAll()
    {
        return _authors;
    }

    public void Add(Author author)
    {
        author.Id = _authors.Count + 1;
        _authors.Add(author);
    }

    public void Update(Author author)
    {
        var existingAuthor = _authors.First(a => a.Id == author.Id);
        existingAuthor.Name = author.Name;
        existingAuthor.Email = author.Email;
    }

    public void Delete(int id)
    {
        var author = _authors.FirstOrDefault(a => a.Id == id);
        if (author != null)
        {
            _authors.Remove(author);
        }
    }

    public Author GetById(int? bookAuthorId)
    {
        return _authors.First(a => a.Id == bookAuthorId);
    }
}