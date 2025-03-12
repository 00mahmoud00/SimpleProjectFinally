using SimpleLibrary.Models;

namespace SimpleLibrary.Services.Authors;

public interface IAuthorService
{
    List<Author> GetAll();
    void Add(Author author);
    void Update(Author author);
    void Delete(int id);
    Author GetById(int? bookAuthorId);
}