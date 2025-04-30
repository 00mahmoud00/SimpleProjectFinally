using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleLibrary.Models;
using SimpleLibrary.Services.IO;
using SimpleLibrary.ViewModels;

namespace SimpleLibrary.Controllers;

public class BookController : Controller
{
    private readonly IFileService _fileService;
    private readonly SimpleLibraryDbContext _context;
    public BookController(IFileService fileService, SimpleLibraryDbContext context)
    {
        _fileService = fileService;
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context
            .Books
            .Select(b => new BooksListViewModel()
            {
                Id = b.Id,
                Name = b.Name,
                AuthorName = b.Author!.Name,
                ImagePath = b.ImagePath,
            }).ToList();
        return View(books);
    }

    public IActionResult Create()
    {
        ViewBag.AuthorList = _context.Authors.Select(a => new SelectListItem()
        {
            Selected = false,
            Text = a.Name,
            Value = a.Id.ToString()
        });
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NewBookViewModel newBookViewModel)
    {
        Console.WriteLine(JsonSerializer.Serialize(newBookViewModel,new JsonSerializerOptions() { WriteIndented = true }));
        var imagePath = _fileService.UploadFile(newBookViewModel.ImageFile,"Books");
        var book = new Book()
        {
            Name = newBookViewModel.Name,
            Description = newBookViewModel.Description,
            AuthorId = newBookViewModel.AuthorId,
            ImagePath = imagePath
        };
        _context.Add(book);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var book = _context.Books
            .Where(b => b.Id == id)
            .Select(b => new BookDetailsViewModel()
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                ImagePath = b.ImagePath,
                Author = new BookDetailsViewModel.AuthorDetails()
                {
                    Id = b.Author!.Id,
                    Name = b.Author.Name,
                    Email = b.Author.Email
                }
            })
            .First();
        return View(book);
    }
    public IActionResult Edit(int id)
    {
        var book = _context
            .Books
            .Where(b => b.Id == id)
            .Select(b => new BookEditViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                AuthorId = b.AuthorId!.Value,
                CurrentImagePath = b.ImagePath
            })
            .First();
            
        ViewBag.AuthorList = _context.Authors.Select(a => new SelectListItem()
        {
            Selected = book.AuthorId == a.Id,
            Text = a.Name,
            Value = a.Id.ToString()
        });
        return View(book);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(BookEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.AuthorList = _context.Authors.Select(a => new SelectListItem()
            {
                Selected = viewModel.AuthorId == a.Id,
                Text = a.Name,
                Value = a.Id.ToString()
            });
            return View(viewModel);
        }
        
        var toUpdate = _context.Books.First(b => b.Id == viewModel.Id);
        var imageUrl = viewModel.ImageFile == null ? toUpdate.ImagePath : _fileService.UploadFile(viewModel.ImageFile, "Books");
        toUpdate.Name = viewModel.Name;
        toUpdate.Description = viewModel.Description;
        toUpdate.AuthorId = viewModel.AuthorId;
        toUpdate.ImagePath = imageUrl;
        
        _context.SaveChanges();
        return RedirectToAction("Details", new { id = viewModel.Id });
    }

    public IActionResult Delete(int id)
    {
        var book = _context.Books.First(b => b.Id == id);
        _context.Books.Remove(book);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}