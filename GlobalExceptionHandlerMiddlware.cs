using SimpleLibrary.Models;
using SimpleLibrary.Services.ExceptionsLog;

namespace SimpleLibrary;

public class GlobalExceptionHandlerMiddlware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlerMiddlware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context,IExceptionLogService exceptionLogService)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            var exptionModel = new ExceptionLog
            {
                DateCreate = DateTime.Now,
                Message = e.Message,
                StackTrace = e.StackTrace!,
            };
            exceptionLogService.Add(exptionModel);
            throw;
        }
    }
}