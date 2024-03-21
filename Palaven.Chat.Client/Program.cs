using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Palaven.Chat;
using Palaven.Chat.Extensions;
using Palaven.Data.Extensions;
using Palaven.Model.Chat;

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
        services.AddChatServices();
    });

var host = hostBuilder.Build();

var chatService = host.Services.GetRequiredService<IChatService>();
var tenantId = new Guid("69A03A54-4181-4D50-8274-D2D88EA911E4");


while (true)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"TenantId[{tenantId}]:");
    var query = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(query))
    {
        break;
    }

    var chatMessage = new ChatMessage
    {
        UserId = tenantId.ToString(),
        Query = query
    };

    var response = chatService.GetChatResponseAsync(chatMessage, CancellationToken.None).GetAwaiter().GetResult();

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"AI CHAT BOT: {response}");
}

host.Run();