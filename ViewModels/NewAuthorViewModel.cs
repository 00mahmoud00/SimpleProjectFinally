namespace SimpleLibrary.ViewModels;

public class NewAuthorViewModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public IFormFile? ImageFile { get; set; }
}