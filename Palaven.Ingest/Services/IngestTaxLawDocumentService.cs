using Liara.Common;
using Palaven.Model.Ingest.Commands;

namespace Palaven.Ingest.Services;

public class IngestTaxLawDocumentService : IIngestTaxLawDocumentService
{
    private readonly ITraceableCommand<IngestLawDocumentModel, IngestLawDocumentTaskInfo> _startTaxLawIngestCommand;
    private readonly ITraceableCommand<ExtractLawDocumentPagesModel, IngestLawDocumentTaskInfo> _extractTaxLawDocumentPagesCommand;

    public IngestTaxLawDocumentService(
        ITraceableCommand<IngestLawDocumentModel, IngestLawDocumentTaskInfo> startTaxLawIngestCommand,
        ITraceableCommand<ExtractLawDocumentPagesModel, IngestLawDocumentTaskInfo> extractTaxLawDocumentPagesCommand)
    {
        _startTaxLawIngestCommand = startTaxLawIngestCommand ?? throw new ArgumentNullException(nameof(startTaxLawIngestCommand));
        _extractTaxLawDocumentPagesCommand = extractTaxLawDocumentPagesCommand ?? throw new ArgumentNullException(nameof(extractTaxLawDocumentPagesCommand));
    }

    public async Task<IResult<IngestLawDocumentTaskInfo>> IngestTaxLawDocumentAsync(IngestLawDocumentModel model, CancellationToken cancellationToken)
    {
        var startIngestResult = await StartTaxLawDocumentIngestAsync(Guid.NewGuid(), model, cancellationToken);
        var extractPagesResult = await ExtractTaxLawDocumentPagesAsync(startIngestResult, cancellationToken);

        return extractPagesResult;
    }

    private Task<IResult<IngestLawDocumentTaskInfo>> StartTaxLawDocumentIngestAsync(Guid operationId, IngestLawDocumentModel model, CancellationToken cancellationToken)
    {
        return _startTaxLawIngestCommand.ExecuteAsync(operationId, model, cancellationToken);
    }

    private async Task<IResult<IngestLawDocumentTaskInfo>> ExtractTaxLawDocumentPagesAsync(IResult<IngestLawDocumentTaskInfo> startLawDocumentResult, CancellationToken cancellationToken)
    {
        if(startLawDocumentResult.AnyErrorsOrValidationFailures)
        {
            return await Task.FromResult(Result<IngestLawDocumentTaskInfo>.Fail(startLawDocumentResult.ValidationErrors, startLawDocumentResult.Errors));
        }
                
        var model = new ExtractLawDocumentPagesModel
        {
            OperationId = startLawDocumentResult.Value.TraceId
        };

        var extractPagesResult = await _extractTaxLawDocumentPagesCommand.ExecuteAsync(model.OperationId, model, cancellationToken);
        return extractPagesResult;
    }
}
