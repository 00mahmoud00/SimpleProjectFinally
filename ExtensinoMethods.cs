using SimpleLibrary.Models;
using SimpleLibrary.ViewModels;

namespace SimpleLibrary;

public static class ExtensinoMethods
{
    public static Author ToAuthor(this AuthorViewModel authorViewModel, string path)
    {
        return new Author()
        {
            Name = authorViewModel.Name,
            Email = authorViewModel.Email,
            ImagePath = path
        };
    }
}