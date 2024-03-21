namespace Liara.CosmosDb;

public class CosmosDbConnectionOptions
{
    public string ConnectionString { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
    public IDictionary<string, string> Containers { get; set; } = new Dictionary<string, string>();
}
