using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Models;

namespace SimpleLibrary.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Console.WriteLine(HttpContext.Connection.RemoteIpAddress!.MapToIPv4());
        return View("Index", HttpContext.Connection.RemoteIpAddress!.MapToIPv4());
    }
    public IActionResult Privacy() => View();

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() 
        => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    public IActionResult ThrowException()
    {
        throw new Exception("Hello My Exception");
        return View();
    }
}