using Liara.CosmosDb;
using Microsoft.Extensions.DependencyInjection;
using Palaven.Model.Ingest.Documents;

namespace Palaven.Data.Extensions;

public static class ApplicationRootExtensions
{
    public static void AddDataServices(this IServiceCollection services)
    {
        services.AddOptions<CosmosDbConnectionOptions>().BindConfiguration("CosmosDB");   

        services.AddSingleton<IDocumentRepository<TaxLawIngestTaskDocument>, TaxLawIngestTaskDocumentRepository>();
        services.AddSingleton<IDocumentRepository<TaxLawToIngestDocument>, TaxLawToIngestDocumentRepository>();
        services.AddSingleton<IDocumentRepository<TaxLawDocumentPage>, TaxLawPageDocumentRepository>();
        services.AddSingleton<IDocumentRepository<TaxLawDocumentArticle>, TaxLawArticleDocumentRepository>();
        services.AddSingleton<IDocumentRepository<TaxLawDocumentGoldenArticle>, TaxLawGoldenArticleDocumentRepository>();
    }
}
