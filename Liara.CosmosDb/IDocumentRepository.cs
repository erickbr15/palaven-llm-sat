using Microsoft.Azure.Cosmos;

namespace Liara.CosmosDb;

public interface IDocumentRepository<TDocument> where TDocument : class
{
    Task<ItemResponse<TDocument>> GetByIdAsync(string id, PartitionKey partitionKey, ItemRequestOptions? itemRequestOptions, CancellationToken cancellationToken);
    Task<IEnumerable<TDocument>> GetAsync(QueryDefinition queryDefinition, string? continuationToken, QueryRequestOptions? queryRequestOptions, CancellationToken cancellationToken);
    Task<ItemResponse<TDocument>> CreateAsync(TDocument item, PartitionKey partitionKey, ItemRequestOptions? itemRequestOptions, CancellationToken cancellationToken);
    Task<ItemResponse<TDocument>> UpsertAsync(TDocument item, PartitionKey partitionKey, ItemRequestOptions? itemRequestOptions, CancellationToken cancellationToken);
    Task DeleteAsync(string id, PartitionKey partitionKey, ItemRequestOptions? itemRequestOptions, CancellationToken cancellationToken);
}
