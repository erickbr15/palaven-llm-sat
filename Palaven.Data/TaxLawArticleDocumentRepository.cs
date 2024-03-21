using Liara.CosmosDb;
using Microsoft.Extensions.Options;
using Palaven.Model.Ingest.Documents;
using Thessia.Expenses.Ingest.Services;

namespace Palaven.Data;

public class TaxLawArticleDocumentRepository : CosmosDbRepository<TaxLawDocumentArticle>
{
    public TaxLawArticleDocumentRepository(IOptions<CosmosDbConnectionOptions> options)
        : base(options, "taxlawdocarticles")
    {
    }
}
