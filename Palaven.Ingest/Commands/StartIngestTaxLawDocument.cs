using Liara.Azure.BlobStorage;
using Liara.Common;
using Liara.CosmosDb;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Palaven.Model.Ingest.Commands;
using Palaven.Model.Ingest.Documents;
using System.Net;

namespace Palaven.Ingest.Commands;

public class StartIngestTaxLawDocument : ITraceableCommand<IngestLawDocumentModel, IngestLawDocumentTaskInfo>
{
    private readonly IBlobStorageService _storageService;
    private readonly IDocumentRepository<TaxLawToIngestDocument> _repository;

    public StartIngestTaxLawDocument(
        IOptions<BlobStorageConnectionOptions> storageOptions, 
        IDocumentRepository<TaxLawToIngestDocument> repository)
    {
        var options = storageOptions?.Value ?? throw new ArgumentNullException(nameof(storageOptions));

        var blobContainerName = options.Containers.TryGetValue(BlobStorageIngestContainers.LawDocsV1, out var containerName) ? 
            containerName : 
            throw new InvalidOperationException($"Unable to find the container name {BlobStorageIngestContainers.LawDocsV1}");

        _storageService = new BlobStorageService(options.ConnectionString, blobContainerName);
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IResult<IngestLawDocumentTaskInfo>> ExecuteAsync(Guid traceId, IngestLawDocumentModel inputModel, CancellationToken cancellationToken)
    {        
        var tenantId = new Guid("69A03A54-4181-4D50-8274-D2D88EA911E4");
        var documentId = Guid.NewGuid();

        var persistedFileName = $"{documentId}{inputModel.FileExtension}";

        var uploadBlobModel = new BlobUploadModel
        {
            BlobContent = inputModel.FileContent,
            BlobName = persistedFileName
        };

        await _storageService.AppendAsync(uploadBlobModel, cancellationToken);

        var lawToIngestDocument = new TaxLawToIngestDocument
        {
            Id = documentId.ToString(),
            TenantId = tenantId.ToString(),
            TraceId = traceId,
            FileName = persistedFileName,
            OriginalFileName = inputModel.FileName,
            LawId = Guid.NewGuid(),
            AcronymLaw = inputModel.Acronym,
            NameLaw = inputModel.Name,
            YearLaw = inputModel.Year,
            LawDocumentVersion = inputModel.LawDocumentVersion,
            DocumentType = nameof(TaxLawToIngestDocument)
        };

        var result = await _repository.CreateAsync(lawToIngestDocument, new PartitionKey(tenantId.ToString()), itemRequestOptions: null, cancellationToken);

        if (result.StatusCode != HttpStatusCode.Created)
        {
            throw new InvalidOperationException($"Unable to create the TaxLawToIngest document. Status code: {result.StatusCode}");
        }

        return new Result<IngestLawDocumentTaskInfo> { Value = new IngestLawDocumentTaskInfo { TraceId = traceId } };
    }    
}
