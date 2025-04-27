using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleLibrary.Models;
using SimpleLibrary.Services.Authors;
using SimpleLibrary.Services.Books;
using SimpleLibrary.Services.IO;
using SimpleLibrary.ViewModels;

namespace SimpleLibrary.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly IFileService _fileService;
    public BookController(IBookService bookService, IAuthorService authorService, IFileService fileService)
    {
        _bookService = bookService;
        _authorService = authorService;
        _fileService = fileService;
    }

    public IActionResult Index()
    {
        var books = _bookService.GetAll();
        Console.WriteLine(JsonSerializer.Serialize(books));
        return View(books);
    }

    public IActionResult Create()
    {
        ViewBag.AuthorList = _authorService.GetAll().Select(a => new SelectListItem()
        {
            Selected = false,
            Text = a.Name,
            Value = a.Id.ToString()
        });
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(BookViewModel bookViewModel)
    {
        Console.WriteLine(JsonSerializer.Serialize(bookViewModel,new JsonSerializerOptions() { WriteIndented = true }));
        var imagePath = _fileService.UploadFile(bookViewModel.ImageFile);
        var book = new Book()
        {
            Name = bookViewModel.Name,
            Description = bookViewModel.Description,
            AuthorId = bookViewModel.AuthorId,
            ImagePath = imagePath
        };
        _bookService.Add(book);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var book = _bookService.GetAll().FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();

        ViewBag.AuthorList = _authorService.GetAll().Select(a => new SelectListItem()
        {
            Selected = book.AuthorId == a.Id,
            Text = a.Name,
            Value = a.Id.ToString()
        });
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Book book)
    {
        _bookService.Update(book);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        _bookService.Delete(id);
        return RedirectToAction("Index");
    }
}