namespace Catalog.Application.Commands.Aws.S3;

public interface IS3Services
{
    Task<bool> ExistsFileAsync(string objectName);

    Task DeleteFileAsync(string objectName);
    
    Task UploadFileAsync(string objectName, MemoryStream stream);
    
    Task UploadFileAsync(string objectName, string file);
    
    Task<string> GetFileAsync(string objectName);
}