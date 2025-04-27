namespace SimpleLibrary.Services.IO;

public interface IFileService
{
    string UploadFile(IFormFile file, string folder = "");
}