using SimpleLibrary.Models;

namespace SimpleLibrary.Services.Authors;

public class AuthorService(SimpleLibraryDbContext context) : IAuthorService
{
    public List<Author> GetAll()
    {
        return context
            .Authors
            .ToList();
    }

    public void Add(Author author)
    {
        context.Authors.Add(author);
        context.SaveChanges();
    }

    public void Update(Author author)
    {
        var existingAuthor = context.Authors.First(a => a.Id == author.Id);
        existingAuthor.Name = author.Name;
        existingAuthor.Email = author.Email;
        Console.WriteLine(context.ChangeTracker.Entries().Select(e => e.State).First());
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var author = context.Authors.FirstOrDefault(a => a.Id == id);
        if (author != null)
        {
            context.Authors.Remove(author);
        }
        Console.WriteLine(context.ChangeTracker.Entries().Select(e => e.State).First());
        context.SaveChanges();
    }

    public Author GetById(int? bookAuthorId)
    {
        return context.Authors.First(a => a.Id == bookAuthorId);
    }
}