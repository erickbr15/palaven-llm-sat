using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.Extensions.Options;

namespace Liara.Azure.AI;

public class DocumentLayoutAnalyzerService : IDocumentLayoutAnalyzerService
{
    private readonly DocumentAnalysisClient _documentAnalysisClient;

    public DocumentLayoutAnalyzerService(IOptions<AiMultiServiceOptions> options)
    {
        var aiMultiServiceOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));        
        _documentAnalysisClient = new DocumentAnalysisClient(new Uri(aiMultiServiceOptions.Endpoint), new AzureKeyCredential(aiMultiServiceOptions.Key));
    }

    public async Task<IReadOnlyList<DocumentTable>> GetTablesAsync(byte[] content, AnalyzeDocumentOptions analyzeDocumentOptions, CancellationToken cancellationToken)
    {
        var streamContent = new MemoryStream(content);            

        var analyzeDocumentOperation = await _documentAnalysisClient.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-layout", streamContent, analyzeDocumentOptions, cancellationToken);
        return analyzeDocumentOperation.Value.Tables;
    }

    public async Task<IReadOnlyList<DocumentPage>> GetPagesAsync(byte[] content, AnalyzeDocumentOptions analyzeDocumentOptions, CancellationToken cancellationToken)
    {
        var streamContent = new MemoryStream(content);            

        var analyzeDocumentOperation = await _documentAnalysisClient.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-layout", streamContent, analyzeDocumentOptions, cancellationToken);
        return analyzeDocumentOperation.Value.Pages;
    }
}
