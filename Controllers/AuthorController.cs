using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Models;
using SimpleLibrary.Services.IO;
using SimpleLibrary.ViewModels;

namespace SimpleLibrary.Controllers;

[Authorize(Roles = "Admin")]
public class AuthorController : Controller
{
    private readonly IFileService _fileService;
    private readonly SimpleLibraryDbContext _context;
    public AuthorController(IFileService fileService, SimpleLibraryDbContext context)
    {
        _fileService = fileService;
        _context = context;
    }

    [ValidateAntiForgeryToken]
    public IActionResult Index()
    {
        var authors = _context
            .Authors
            .Select(a => new AuthorsListViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.Email,
                ImagePath = a.ImagePath,
            }).ToList();
        return View(authors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NewAuthorViewModel newAuthorViewModel)
    {
        var filePath = _fileService.UploadFile(newAuthorViewModel.ImageFile!,"Authors");
        var author = new Author()
        {
            Name = newAuthorViewModel.Name,
            Email = newAuthorViewModel.Email,
            ImagePath = filePath
        };
        _context.Authors.Add(author);
        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var author = _context.Authors
            .Where(a => a.Id == id)
            .Select(a => new AuthorEditViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.Email,
                CurrentImagePath = a.ImagePath
            })
            .First();
        return View(author);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AuthorEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);
    
        var authorToUpdate = _context.Authors.First(a => a.Id == viewModel.Id);
        var imageUrl = viewModel.ImageFile == null ? authorToUpdate.ImagePath : _fileService.UploadFile(viewModel.ImageFile, "Authors");
        authorToUpdate.Name = viewModel.Name;
        authorToUpdate.Email = viewModel.Email;
        authorToUpdate.ImagePath = imageUrl;
        
        _context.SaveChanges();
        return RedirectToAction("Details", new { id = viewModel.Id });
    }

    public IActionResult Details(int id)
    {
        var author = _context.Authors
            .Where(a => a.Id == id)
            .Select(a => new AuthorDetailsViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.Email,
                ImagePath = a.ImagePath,
                Books = a.Books.Select(b => new AuthorDetailsViewModel.BookDetails()
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList()
            })
            .First();
        Console.WriteLine(JsonSerializer.Serialize(author));
        return View(author);
    }

    public IActionResult Delete(int id)
    {
        var author = _context.Authors.First(a => a.Id == id);
        _context.Authors.Remove(author);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}