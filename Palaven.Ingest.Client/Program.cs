using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Palaven.Data.Extensions;
using Palaven.Ingest.Extensions;

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
        services.AddAIServices();
        services.AddDataServices();
        services.AddIngestServices();        
    });

var host = hostBuilder.Build();

/*
 * CREATES THE BRONZE DOCUMENT
var lawDocumentFilePath = @"C:\github-code\palaven-sat\law-documents\v1\LISR-2024.pdf";
var fileInfo = new FileInfo(lawDocumentFilePath);
var fileContent = File.ReadAllBytes(fileInfo.FullName);
var fileName = fileInfo.Name;

var model = new IngestLawDocumentModel
{
    Acronym = "LISR",
    LawDocumentVersion = "v1.plain-2024",
    Year = 2024,
    Name = "Ley del Impuesto Sobre la Renta",
    FileContent = fileContent,
    FileName = fileName,
    FileExtension = fileInfo.Extension
};

var ingestService = host.Services.GetRequiredService<IIngestTaxLawDocumentService>();
var result = ingestService.IngestTaxLawDocumentAsync(model, CancellationToken.None).GetAwaiter().GetResult();
*/

/*
 * CREATES THE SILVER DOCUMENT
var extractCommand = host.Services.GetRequiredService<ITraceableCommand<ExtractLawDocumentArticlesModel, IngestLawDocumentTaskInfo>>();

var traceId = new Guid("b4ac9acb-a80a-425b-8dd0-88bb2c748949");
var model = new ExtractLawDocumentArticlesModel { OperationId = traceId };
var result = extractCommand.ExecuteAsync(traceId, model, CancellationToken.None).GetAwaiter().GetResult();
*/

/*
 * CREATES THE GOLDEN DOCUMENT
var createGoldenArticleCommand = host.Services.GetRequiredService<ITraceableCommand<CreateGoldenArticleDocumentModel, Guid>>();
var traceId = new Guid("b4ac9acb-a80a-425b-8dd0-88bb2c748949");
var articleIds = new List<string> 
{
    "abf67e78-a2ad-42ff-b852-7b621e8d6e9d", "0e4337aa-c77e-4e70-b490-cbea1ee667b2",
    "13a9135e-603b-40f4-95c1-2528963fdbae", "3816e261-fb8d-432d-a159-161362e05589",
    "31a0f1b4-e4b7-4848-9785-926b8d8f59f0", "78a99177-dc7c-4275-9e94-d9b64202b48d",
    "0556e68d-0fd5-4562-a09a-64fe0560d2df", "6fd9db9f-adde-4dc2-ad7f-6974fd052e92",
    "5e573ef3-4c8c-4749-9870-6a479cce8e10", "e2d77d72-335f-4a3c-8ab4-7d273a55d2b6",
    "45152908-664d-4b4f-a863-a7cb304e347d"
};

foreach (var id in articleIds)
{
    var model = new CreateGoldenArticleDocumentModel { ArticleId = new Guid(id) };
    var result = createGoldenArticleCommand.ExecuteAsync(traceId, model, CancellationToken.None).GetAwaiter().GetResult();
}
*/

host.Run();
