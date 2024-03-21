using Liara.Pinecone.Model;

namespace Liara.Pinecone;

public interface IPineconeServiceClient
{
    Task UpsertAsync(UpsertDataModel inputModel, CancellationToken cancellationToken);

    Task<QueryVectorsResult?> QueryVectorsAsync(QueryVectorsModel inputModel, CancellationToken cancellationToken);
}
