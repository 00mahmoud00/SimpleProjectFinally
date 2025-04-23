namespace SimpleLibrary.Models;

public class ExceptionLog
{
    public int Id { get; set; }
    public DateTime DateCreate { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
}