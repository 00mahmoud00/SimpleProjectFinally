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

    // List all authors
    public IActionResult Index()
    {
        var authors = _authorService.GetAll();
        return View(authors);
    }

    // Create new author
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Author author)
    {
        {
            _authorService.Add(author);
            return RedirectToAction("Index");
        }
        return View(author);
    }

    // Edit author details
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
        // if (ModelState.IsValid)
        {
            _authorService.Update(author);
            return RedirectToAction("Index");
        }
        return View(author);
    }

    // View details of the author and their books
    public IActionResult Details(int id)
    {
        var author = _authorService.GetAll().FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();

        var books = _bookService.GetAll().Where(b => b.AuthorId == id).ToList();
        author.Books = books;

        return View(author);
    }

    // Delete author
    public IActionResult Delete(int id)
    {
        var author = _authorService.GetAll().FirstOrDefault(a => a.Id == id);
        _authorService.Delete(id);
        // if (author == null) return NotFound();
        return RedirectToAction("Index");
    }
}