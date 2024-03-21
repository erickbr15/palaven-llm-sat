﻿using Liara.CosmosDb;
using Microsoft.Extensions.Options;
using Palaven.Model.Ingest.Documents;
using Thessia.Expenses.Ingest.Services;

namespace Palaven.Data;

public class TaxLawGoldenArticleDocumentRepository : CosmosDbRepository<TaxLawDocumentGoldenArticle>
{
    public TaxLawGoldenArticleDocumentRepository(IOptions<CosmosDbConnectionOptions> options)
        : base(options, "taxlawgoldenarticles")
    {
    }
}