using Azure.Identity;
using Liara.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Palaven.Data.Extensions;
using Palaven.Model.VectorIndexing.Commands;

var hostBuilder = new HostBuilder()
    .ConfigureAppConfiguration((hostingContext, configBuilder) =>
    {
        configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var config = configBuilder.Build();
        var appConfigurationConnectionString = config.GetConnectionString("AppConfiguration");

        configBuilder.AddAzureAppConfiguration(options =>
        {
            options.Connect(appConfigurationConnectionString).Select(KeyFilter.Any);
            options.ConfigureKeyVault(kv =>
            {
                kv.SetCredential(new DefaultAzureCredential());
            });
        });
    })
    .ConfigureServices((hostContext, services) =>
    {        
        services.AddDataServices();        
    });

var host = hostBuilder.Build();

var uploadVectorIndex = host.Services.GetRequiredService<ITraceableCommand<UploadGoldenArticleToVectorIndexModel, Guid>>();

var traceId = new Guid("b4ac9acb-a80a-425b-8dd0-88bb2c748949");
var articleIds = new List<string>
{
    "b1ca570e-01a2-4e89-9dd7-708f926603e1", "2172d8e2-3430-47a8-b869-01fb314489ab",
    "486374db-9b4d-4442-8f63-f0cd118f040f", "0d138874-7055-4e7f-9320-c4852e7c433f",
    "076271e0-2c2d-42c1-8e69-7bf95e34a98e", "72798184-c3f1-42f6-9069-7e7ae289cff5",
    "7308c36e-911f-4f49-b69c-aa82ff5a9ecb", "4dd05906-df02-4813-8585-b29e094a9ceb",
    "b0ba6174-8522-40c0-8193-f220c2f71982", "1167c98a-c979-407a-92a0-7829b74cf027",
    "e43a2c45-0246-4d69-a0f1-84f3a2702e4a"
};

foreach (var article in articleIds)
{
    var goldenArticleId = new Guid(article);

    uploadVectorIndex.ExecuteAsync(traceId,
        new UploadGoldenArticleToVectorIndexModel { GoldenArticleId = goldenArticleId }, CancellationToken.None).GetAwaiter().GetResult();
}


host.Run();
