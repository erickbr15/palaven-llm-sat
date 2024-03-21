using Liara.CosmosDb;
using Microsoft.Extensions.Options;
using Palaven.Model.Ingest.Documents;
using Thessia.Expenses.Ingest.Services;

namespace Palaven.Data;

public class TaxLawPageDocumentRepository : CosmosDbRepository<TaxLawDocumentPage>
{
    public TaxLawPageDocumentRepository(IOptions<CosmosDbConnectionOptions> options) 
        : base(options, "taxlawdocpages")
    {                
    }
}
