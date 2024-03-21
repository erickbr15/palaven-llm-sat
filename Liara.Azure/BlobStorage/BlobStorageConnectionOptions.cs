namespace Liara.Azure.BlobStorage;

public class BlobStorageConnectionOptions
{
    public string ConnectionString { get; set; } = default!;
    public IDictionary<string, string> Containers { get; set; } = new Dictionary<string, string>();
}
