using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleLibrary.Models;
using SimpleLibrary.Services.Authors;
using SimpleLibrary.Services.Books;

namespace SimpleLibrary.Controllers;

public class BookController(IBookService bookService, IAuthorService authorService) : Controller
{
    public IActionResult Index()
    {
        var books = bookService.GetAll();
        return View(books);
    }

    public IActionResult Create()
    {
        ViewBag.AuthorList = authorService.GetAll().Select(a => new SelectListItem()
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
        book.Author = authorService.GetById(book.AuthorId);
        bookService.Add(book);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var book = bookService.GetAll().FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();

        ViewBag.AuthorList = authorService.GetAll().Select(a => new SelectListItem()
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
        bookService.Update(book);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        bookService.Delete(id);
        return RedirectToAction("Index");
    }
}