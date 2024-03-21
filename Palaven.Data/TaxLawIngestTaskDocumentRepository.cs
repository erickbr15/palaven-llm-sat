using Liara.CosmosDb;
using Microsoft.Extensions.Options;
using Palaven.Model.Ingest.Documents;
using Thessia.Expenses.Ingest.Services;

namespace Palaven.Data;

public class TaxLawIngestTaskDocumentRepository : CosmosDbRepository<TaxLawIngestTaskDocument>
{
    public TaxLawIngestTaskDocumentRepository(IOptions<CosmosDbConnectionOptions> options) 
        : base(options, "ingesttasks")
    {
    }
}
