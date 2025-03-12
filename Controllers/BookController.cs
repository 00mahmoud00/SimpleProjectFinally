using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleLibrary.Models;
using SimpleLibrary.Services.Authors;
using SimpleLibrary.Services.Books;

namespace SimpleLibrary.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;

    public BookController(IBookService bookService, IAuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }

    // List all books
    public IActionResult Index()
    {
        var books = _bookService.GetAll();
        return View(books);
    }

    // Create a new book
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
    public IActionResult Create(Book book)
    {
        book.Author = _authorService.GetById(book.AuthorId);
        _bookService.Add(book);
        return RedirectToAction("Index");
        ViewData["Authors"] = _authorService.GetAll();
        return View(book);
    }

    // Edit book details
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
        // if (ModelState.IsValid)
        {
            _bookService.Update(book);
            return RedirectToAction("Index");
        }

        ViewData["Authors"] = _authorService.GetAll();
        return View(book);
    }

    // Delete a book
    public IActionResult Delete(int id)
    {
        _bookService.Delete(id);
        return RedirectToAction("Index");
    }

    // [HttpPost, ActionName("Delete")]
    // [ValidateAntiForgeryToken]
    // public IActionResult DeleteConfirmed(int id)
    // {
    //     _bookService.Delete(id);
    //     return RedirectToAction("Index");
    // }
}