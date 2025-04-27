using System.Xml.XPath;

namespace SimpleLibrary.Services.IO;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string UploadFile(IFormFile file, string folder = "")
    {
        var wwwroot = _webHostEnvironment.WebRootPath;
        if(string.IsNullOrEmpty(wwwroot))
            throw new Exception("wwwroot is null");
        var random = Guid.NewGuid();
        
        var fileName = $"{random}.{file.FileName.Split('.').Last()}";
        var filePath = Path.Combine(wwwroot, folder, fileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(stream);
        
        return Path.Combine(folder, fileName);
    }

    public void DeleteFile(string relativePath)
    {
        var wwwroot = _webHostEnvironment.WebRootPath;
        if (string.IsNullOrEmpty(wwwroot))
            throw new Exception("wwwroot is null");

        var filePath = Path.Combine(wwwroot, relativePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else
        {
            throw new FileNotFoundException("File not found at the specified path.", filePath);
        }
    }
}