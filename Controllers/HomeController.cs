using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Models;

namespace SimpleLibrary.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SimpleLibraryDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, SimpleLibraryDbContext context, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Index()
    {
        var books = await _context
            .Books
            .Select(book => new BookViewModel
            {
                Id = book.Id,
                Title = book.Name,
                Author = book.Author!.Name,
                IsBorrowed = book.Borrows.Any(b => b.ReturnDate == null)
            })
            .ToListAsync();

        return View(books);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() 
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    public IActionResult Welcome() => View();
    
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> Borrow(int bookId)
    {
        var user = await _userManager.GetUserAsync(User);
        var book = await _context
        .Books
        .Include(b => b.Borrows)
        .FirstAsync(a => a.Id == bookId);
        if (book.Borrows.Any(b => b.ReturnDate == null))
            return BadRequest("Book is not available.");
        var borrow = new Borrow
        {
            UserId = user.Id,
            BookId = book.Id,
            BorrowDate = DateTime.Now
        };
        _context.Borrows.Add(borrow);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Borrows()
    {
        var borrows = await _context.Borrows.Include(b => b.Book).ToListAsync();
        return View(borrows);
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> History()
    {
        var user = await _userManager.GetUserAsync(User);
        var history = 
            await _context
                .Borrows
            .Where(b => b.UserId == user.Id)
            .Select(b => new HistoryViewModel
            {
                BorrowId = b.Id,
                BookTitle = b.Book.Name,
                BorrowDate = b.BorrowDate,
                ReturnDate = b.ReturnDate
            })
            .ToListAsync();

        return View(history);
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> EndBorrow(int borrowId)
    {
        var borrow = await _context
            .Borrows
            .FirstOrDefaultAsync(b => b.Id == borrowId);
        
        borrow.ReturnDate = DateTime.Now;
        await _context.SaveChangesAsync();

        return RedirectToAction("History");
    }
}