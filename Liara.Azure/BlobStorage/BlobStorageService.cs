using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using AppendBlobCreateOptions = Azure.Storage.Blobs.Models.AppendBlobCreateOptions;

namespace Liara.Azure.BlobStorage;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _blobContainerClient;

    public BlobStorageService(string connectionString, string blobContainerName)
    {
        if(string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }

        if(string.IsNullOrWhiteSpace(blobContainerName))
        {
            throw new ArgumentNullException(nameof(blobContainerName));
        }

        _blobContainerClient = new BlobContainerClient(connectionString, blobContainerName);
    }

    public async Task AppendAsync(BlobUploadModel uploadingModel, CancellationToken cancellationToken)
    {
        var blobClient = _blobContainerClient.GetAppendBlobClient(uploadingModel.BlobName);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);        
        await blobClient.CreateIfNotExistsAsync(new AppendBlobCreateOptions(), cancellationToken);

        var maxBlockSize = blobClient.AppendBlobMaxAppendBlockBytes;
        var contentStream = new MemoryStream(uploadingModel.BlobContent);

        if (contentStream.Length >= maxBlockSize)
        {
            throw new InvalidOperationException($"Unable to process the file. It exceeds the allowed max length.");
        }

        await blobClient.AppendBlockAsync(contentStream, cancellationToken: cancellationToken);
    }

    public async Task<byte[]> ReadAsync(string blobName, CancellationToken cancellationToken)
    {
        var blobClient = _blobContainerClient.GetBlobClient(blobName);

        var blobDownloadInfo = await blobClient.DownloadAsync(cancellationToken);

        using var memoryStream = new MemoryStream();

        await blobDownloadInfo.Value.Content.CopyToAsync(memoryStream, cancellationToken);

        return memoryStream.ToArray();
    }
}
