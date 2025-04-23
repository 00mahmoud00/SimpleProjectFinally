using SimpleLibrary.Models;

namespace SimpleLibrary.Services.ExceptionsLog;

public interface IExceptionLogService
{
    List<ExceptionLog> GetAll();
    void Add(ExceptionLog exception);
}