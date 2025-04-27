namespace SimpleLibrary.ViewModels;

public class AuthorDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public List<BookDetails> Books { get; set; } = [];
    public class BookDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}