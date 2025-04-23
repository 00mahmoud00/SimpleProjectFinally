using Microsoft.AspNetCore.Mvc;
using SimpleLibrary.Services.ExceptionsLog;

namespace SimpleLibrary.Controllers;

public class ExceptionController : Controller
{
    private readonly IExceptionLogService  _exceptionLogService;

    public ExceptionController(IExceptionLogService exceptionLogService)
    {
        _exceptionLogService = exceptionLogService;
    }

    public IActionResult Index()
    {
        var logs = _exceptionLogService.GetAll();
        return View(logs);
    }
}