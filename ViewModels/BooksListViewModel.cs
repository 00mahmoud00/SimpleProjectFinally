namespace SimpleLibrary.ViewModels;

public class BooksListViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
}