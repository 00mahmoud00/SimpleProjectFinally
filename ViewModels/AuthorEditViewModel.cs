using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SimpleLibrary.ViewModels;

public class AuthorEditViewModel
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    public string CurrentImagePath { get; set; }
    
    public IFormFile ImageFile { get; set; }
}
