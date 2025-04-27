using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SimpleLibrary.ViewModels;

public class BookEditViewModel
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int AuthorId { get; set; }
    
    public string CurrentImagePath { get; set; }
    
    public IFormFile ImageFile { get; set; }
}
