namespace Liara.Azure.BlobStorage;

public interface IBlobStorageService
{
    Task AppendAsync(BlobUploadModel uploadingModel, CancellationToken cancellationToken);
    Task<byte[]> ReadAsync(string blobName, CancellationToken cancellationToken);
}
