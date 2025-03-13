using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Models;
using SimpleLibrary.Services.Authors;
using SimpleLibrary.Services.Books;

namespace SimpleLibrary.Controllers;

public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;
    private readonly IBookService _bookService;

    public AuthorController(IAuthorService authorService, IBookService bookService)
    {
        _authorService = authorService;
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        var authors = _authorService.GetAll();
        return View(authors);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Author author)
    {
        _authorService.Add(author);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var author = _authorService.GetAll().FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();
        return View(author);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Author author)
    {
        _authorService.Update(author);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var author = _authorService.GetAll().FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();

        var books = _bookService.GetAll().Where(b => b.AuthorId == id).ToList();
        author.Books = books;

        return View(author);
    }

    public IActionResult Delete(int id)
    {
        var author = _authorService.GetAll().FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();
        _authorService.Delete(id);
        return RedirectToAction("Index");
    }
}