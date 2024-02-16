using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Commands.Aws.S3;

public class S3Services(IAmazonS3 amazonS3, ILogger<S3Services> logger) : IS3Services
{
    private readonly string? _bucketName = Environment.GetEnvironmentVariable("AWS__S3__BUCKET__NAME");

    public async Task<bool> ExistsFileAsync(string objectName)
    {
        try
        {
            var request = new GetObjectMetadataRequest
            {
                BucketName = _bucketName,
                Key = objectName,
            };

            await amazonS3.GetObjectMetadataAsync(request).ConfigureAwait(false);
            return true;
        }
        catch (AmazonS3Exception)
        {
            logger.LogError("Arquivo não encontrado.");
            return false;
        }
    }

    public async Task DeleteFileAsync(string objectName)
    {
        var existsFile = await ExistsFileAsync(objectName).ConfigureAwait(false);
        if (existsFile)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = objectName,
                };

                await amazonS3.DeleteObjectAsync(deleteObjectRequest).ConfigureAwait(false);
            }
            catch (AmazonS3Exception ex)
            {
                logger.LogError(ex, "Error ao deletar arquivo.");
            }
        }
    }

    public async Task UploadFileAsync(string objectName, MemoryStream stream)
    {
        try
        {
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = objectName,
                InputStream = stream
            };

            await amazonS3.PutObjectAsync(request).ConfigureAwait(false);
        }
        catch (AmazonS3Exception ex)
        {
            logger.LogError(ex, "Error ao enviar o arquivo para o servidor.");
        }
    }

    public async Task<MemoryStream> GetFileAsync(string objectName)
    {
        var memoryStream = new MemoryStream();

        try
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = objectName
            };

            using var response = await amazonS3.GetObjectAsync(request).ConfigureAwait(false);
            await using var responseStream = response.ResponseStream;

            await responseStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
        }
        catch (AmazonS3Exception e)
        {
            logger.LogError(e, "Error ao baixar o arquivo.");
        }

        return memoryStream;
    }
}