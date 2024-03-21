using Liara.Azure.AI;
using Liara.Azure.BlobStorage;
using Liara.Common.Http;
using Liara.CosmosDb;
using Liara.OpenAI;
using Liara.Pinecone;
using Microsoft.Extensions.DependencyInjection;

namespace Palaven.Chat.Extensions;

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

    public static void AddChatServices(this IServiceCollection services)
    {
        services.AddSingleton<IChatService, ChatService>();
    }
}
