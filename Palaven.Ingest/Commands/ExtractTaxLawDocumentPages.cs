using Azure.AI.FormRecognizer.DocumentAnalysis;
using Liara.Azure.AI;
using Liara.Azure.BlobStorage;
using Liara.Common;
using Liara.CosmosDb;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Palaven.Model.Ingest.Commands;
using Palaven.Model.Ingest.Documents;
using System.Net;

namespace Palaven.Ingest.Commands;

public class ExtractTaxLawDocumentPages : ITraceableCommand<ExtractLawDocumentPagesModel, IngestLawDocumentTaskInfo>
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly IDocumentRepository<TaxLawToIngestDocument> _lawDocumentToIngestRepository;
    private readonly IDocumentRepository<TaxLawDocumentPage> _lawPageDocumentRepository;
    private readonly IDocumentLayoutAnalyzerService _documentAnalyzer;

    public ExtractTaxLawDocumentPages(IOptions<BlobStorageConnectionOptions> storageOptions,
        IDocumentRepository<TaxLawToIngestDocument> lawDocumentToIngestRepository,
        IDocumentRepository<TaxLawDocumentPage> lawPageDocumentRepository,
        IDocumentLayoutAnalyzerService documentAnalyzer)
    {   
        var options = storageOptions?.Value ?? throw new ArgumentNullException(nameof(storageOptions));

        var blobContainerName = options.Containers.TryGetValue(BlobStorageIngestContainers.LawDocsV1, out var containerName) ?
            containerName :
            throw new InvalidOperationException($"Unable to find the container name {BlobStorageIngestContainers.LawDocsV1}");

        _blobStorageService = new BlobStorageService(options.ConnectionString, blobContainerName);
        _lawDocumentToIngestRepository = lawDocumentToIngestRepository ?? throw new ArgumentNullException(nameof(lawDocumentToIngestRepository));
        _lawPageDocumentRepository = lawPageDocumentRepository ?? throw new ArgumentNullException(nameof(lawPageDocumentRepository));
        _documentAnalyzer = documentAnalyzer ?? throw new ArgumentNullException(nameof(documentAnalyzer));
    }

    public async Task<IResult<IngestLawDocumentTaskInfo>> ExecuteAsync(Guid traceId, ExtractLawDocumentPagesModel inputModel, CancellationToken cancellationToken)
    {
        var tenantId = new Guid("69A03A54-4181-4D50-8274-D2D88EA911E4");

        var query = new QueryDefinition($"SELECT * FROM c WHERE c.TraceId = \"{traceId}\"");

        var queryResults = await _lawDocumentToIngestRepository.GetAsync(query, continuationToken: null,
            new QueryRequestOptions { PartitionKey = new PartitionKey(tenantId.ToString()) },            
            cancellationToken);
        
        var taxLawToIngestDocument = queryResults.SingleOrDefault() ?? throw new InvalidOperationException($"Unable to find the tax law document to ingest with trace id {traceId}");

        var blobBytes = await _blobStorageService.ReadAsync(taxLawToIngestDocument.FileName, cancellationToken);
        var documentPages = await ExtractPagesAsync(blobBytes, cancellationToken);
        
        foreach (var page in documentPages)
        {            
            var taxLawPage = new TaxLawDocumentPage();

            taxLawPage.Id = Guid.NewGuid().ToString();
            taxLawPage.TenantId = tenantId.ToString();
            taxLawPage.TraceId = traceId;
            taxLawPage.DocumentType = nameof(TaxLawDocumentPage);
            taxLawPage.LawDocumentVersion = taxLawToIngestDocument.LawDocumentVersion;
            taxLawPage.LawId = taxLawToIngestDocument.LawId;
            taxLawPage.PageNumber = page.PageNumber;

            var lineCounter = 0;
            taxLawPage.Lines = page.Lines.Select(l =>
            {
                var line = new TaxLawDocumentLine
                {
                    PageDocumentId = new Guid(taxLawPage.Id),
                    PageNumber = taxLawPage.PageNumber,
                    LineNumber = ++lineCounter,
                    LineId = Guid.NewGuid(),
                    Content = l.Content
                };

                line.BoundingBox.AddRange(l.BoundingPolygon.ToList());

                return line;
            }).ToList();

            var result = await _lawPageDocumentRepository.CreateAsync(taxLawPage, 
                new PartitionKey(tenantId.ToString()), 
                itemRequestOptions: null, 
                cancellationToken);

            if (result.StatusCode != HttpStatusCode.Created)
            {
                throw new InvalidOperationException($"Unable to create the account file document. Status code: {result.StatusCode}");
            }
        }                        

        return new Result<IngestLawDocumentTaskInfo> { Value = new IngestLawDocumentTaskInfo { TraceId = traceId } };
    }

    private async Task<IList<DocumentPage>> ExtractPagesAsync(byte[] taxLawContent, CancellationToken cancellationToken)
    {
        var analysisOptions = new AnalyzeDocumentOptions
        {
            Pages = { "1-15" }
        };

        var analysisResult = await _documentAnalyzer.GetPagesAsync(taxLawContent, analysisOptions, cancellationToken);

        return analysisResult.ToList();
    }    
}
