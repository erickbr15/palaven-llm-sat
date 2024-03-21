using Liara.Azure.AI;
using Liara.Common;
using Liara.CosmosDb;
using Microsoft.Extensions.DependencyInjection;
using Liara.Azure.BlobStorage;
using Palaven.Model.Ingest.Commands;
using Palaven.Ingest.Commands;
using Palaven.Ingest.Services;
using Liara.OpenAI;
using Liara.Common.Http;
using Liara.Pinecone;

namespace Palaven.Ingest.Extensions;

public static class ApplicationRootExtensions
{
    public static void AddAIServices(this IServiceCollection services)
    {
        services.AddOptions<BlobStorageConnectionOptions>().BindConfiguration("BlobStorage");
        services.AddOptions<CosmosDbConnectionOptions>().BindConfiguration("CosmosDB");
        services.AddOptions<AiMultiServiceOptions>().BindConfiguration("AiServices");
        services.AddOptions<OpenAiOptions>().BindConfiguration("OpenAi");
        services.AddOptions<PineconeOptions>().BindConfiguration("Pinecone");

        services.AddSingleton<IHttpProxy, HttpProxy>();
        services.AddSingleton<IOpenAiServiceClient, OpenAiServiceClient>();
        services.AddSingleton<IPineconeServiceClient, PineconeServiceClient>();
        services.AddSingleton<IDocumentLayoutAnalyzerService, DocumentLayoutAnalyzerService>();
    }

    public static void AddIngestServices(this IServiceCollection services)
    {       
        services.AddSingleton<ITraceableCommand<IngestLawDocumentModel, IngestLawDocumentTaskInfo>, StartIngestTaxLawDocument>();
        services.AddSingleton<ITraceableCommand<ExtractLawDocumentPagesModel, IngestLawDocumentTaskInfo>, ExtractTaxLawDocumentPages>();
        services.AddSingleton<ITraceableCommand<ExtractLawDocumentArticlesModel, IngestLawDocumentTaskInfo>, ExtractTaxLawDocumentArticles>();
        services.AddSingleton<ITraceableCommand<CreateGoldenArticleDocumentModel, Guid>, CreateTaxLawGoldenArticleDocument>();        

        services.AddSingleton<IIngestTaxLawDocumentService, IngestTaxLawDocumentService>();
    }
}
