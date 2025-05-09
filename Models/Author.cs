namespace SimpleLibrary.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImagePath { get; set; }
    public List<Book> Books { get; set; }
}