namespace SimpleLibrary.ViewModels;

public class BookViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile ImageFile { get; set; }
    public int AuthorId { get; set; }
}