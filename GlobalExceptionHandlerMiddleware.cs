using SimpleLibrary.Models;
using SimpleLibrary.Services.ExceptionsLog;

namespace SimpleLibrary;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly IExceptionLogService _exceptionLogService;

    public GlobalExceptionHandlerMiddleware(IExceptionLogService exceptionLogService)
    {
        _exceptionLogService = exceptionLogService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var exptionModel = new ExceptionLog
            {
                DateCreate = DateTime.Now,
                Message = e.Message,
                StackTrace = e.StackTrace!,
            };
            _exceptionLogService.Add(exptionModel);
            throw;
        }
    }
}