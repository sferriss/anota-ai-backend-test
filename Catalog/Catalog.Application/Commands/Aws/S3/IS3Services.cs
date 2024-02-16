namespace Catalog.Application.Commands.Aws.S3;

public interface IS3Services
{
    Task<bool> ExistsFileAsync(string objectName);

    Task DeleteFileAsync(string objectName);
    
    Task UploadFileAsync(string objectName, MemoryStream stream);
    
    Task<MemoryStream> GetFileAsync(string objectName);
}