using Liara.CosmosDb;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Thessia.Expenses.Ingest.Services;

public class CosmosDbRepository<TDocument> : IDocumentRepository<TDocument> where TDocument : class
{
    private readonly Container _container;

    public CosmosDbRepository(IOptions<CosmosDbConnectionOptions> options, string containerName)
    {
        if (string.IsNullOrWhiteSpace(containerName))
        {
            throw new ArgumentNullException(nameof(containerName));
        }
        
        var cosmosConnectionOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        var client = new CosmosClient(cosmosConnectionOptions.ConnectionString);
        
        _container = client.GetContainer(cosmosConnectionOptions.DatabaseName, containerName);
    }

    public async Task<ItemResponse<TDocument>> GetByIdAsync(string id, PartitionKey partitionKey, ItemRequestOptions? itemRequestOptions, CancellationToken cancellationToken)
    {
        var response = await _container.ReadItemAsync<TDocument>(id, partitionKey, itemRequestOptions, cancellationToken);
        return response;
    }

    public async Task<IEnumerable<TDocument>> GetAsync(QueryDefinition queryDefinition, string? continuationToken, QueryRequestOptions? queryRequestOptions, CancellationToken cancellationToken)
    {
        var query = _container.GetItemQueryIterator<TDocument>(queryDefinition, continuationToken, queryRequestOptions);
        var results = new List<TDocument>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync(cancellationToken);
            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task<ItemResponse<TDocument>> CreateAsync(TDocument item, PartitionKey partitionKey, ItemRequestOptions? itemRequestOptions, CancellationToken cancellationToken)
    {
        var response = await _container.CreateItemAsync(item, partitionKey, itemRequestOptions, cancellationToken);
        return response;
    }

    public async Task<ItemResponse<TDocument>> UpsertAsync(TDocument item, PartitionKey partitionKey, ItemRequestOptions? itemRequestOptions, CancellationToken cancellationToken)
    {
        var response = await _container.UpsertItemAsync(item, partitionKey, itemRequestOptions, cancellationToken);
        return response;
    }

    public async Task DeleteAsync(string id, PartitionKey partitionKey, ItemRequestOptions? itemRequestOptions, CancellationToken cancellationToken)
    {
        await _container.DeleteItemAsync<TDocument>(id, partitionKey, itemRequestOptions, cancellationToken);
    }
}