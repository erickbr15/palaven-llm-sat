using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace Liara.Azure.AI;

public interface IDocumentLayoutAnalyzerService
{
    Task<IReadOnlyList<DocumentTable>> GetTablesAsync(byte[] content, AnalyzeDocumentOptions analyzeDocumentOptions, CancellationToken cancellationToken);
    Task<IReadOnlyList<DocumentPage>> GetPagesAsync(byte[] content, AnalyzeDocumentOptions analyzeDocumentOptions, CancellationToken cancellationToken);
}
