namespace Liara.Azure.BlobStorage;

public sealed class BlobUploadModel
{
    public string BlobName { get; set; } = default!;
    public byte[] BlobContent { get; set; } = default!;
}
