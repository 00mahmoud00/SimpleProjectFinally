using SimpleLibrary.Models;
using SimpleLibrary.ViewModels;

namespace SimpleLibrary;

public static class ExtensinoMethods
{
    public static Author ToAuthor(this NewAuthorViewModel newAuthorViewModel, string path)
    {
        return new Author()
        {
            Name = newAuthorViewModel.Name,
            Email = newAuthorViewModel.Email,
            ImagePath = path
        };
    }
}