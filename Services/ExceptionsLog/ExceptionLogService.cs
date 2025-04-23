using SimpleLibrary.Models;

namespace SimpleLibrary.Services.ExceptionsLog;

public class ExceptionLogService : IExceptionLogService
{
    private readonly SimpleLibraryDbContext _context;

    public ExceptionLogService(SimpleLibraryDbContext context)
    {
        _context = context;
    }

    public List<ExceptionLog> GetAll()
    {
        return _context.ExceptionLogs.ToList();
    }

    public void Add(ExceptionLog exception)
    {
        _context.ExceptionLogs.Add(exception);
        _context.SaveChanges();
    }
}