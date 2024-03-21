using Liara.Common;
using Palaven.Model.Ingest.Commands;

namespace Palaven.Ingest.Services;

public interface IIngestTaxLawDocumentService
{
    Task<IResult<IngestLawDocumentTaskInfo>> IngestTaxLawDocumentAsync(IngestLawDocumentModel model, CancellationToken cancellationToken);
}
