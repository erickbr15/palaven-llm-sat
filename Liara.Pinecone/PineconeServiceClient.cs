using Liara.Common.Http;
using Liara.Pinecone.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Liara.Pinecone;

public class PineconeServiceClient : IPineconeServiceClient
{
    private readonly IHttpProxy _httpProxy;
    private readonly PineconeOptions _pineconeOptions;

    public PineconeServiceClient(IOptions<PineconeOptions> pineconeOptions, IHttpProxy httpProxy)
    {
        _pineconeOptions = pineconeOptions?.Value ?? throw new ArgumentNullException(nameof(pineconeOptions));
        _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
    }

    public async Task<QueryVectorsResult?> QueryVectorsAsync(QueryVectorsModel inputModel, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>
        {
            { "Api-Key", _pineconeOptions.ApiKey }
        };

        string content = JsonConvert.SerializeObject(inputModel);
        var buffer = Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var upsertEndpointUrl = $"{_pineconeOptions.IndexHostUrl}/query";

        var response = await _httpProxy.PostAsync(new Uri(upsertEndpointUrl),
            headers,
            byteContent,
            cancellationToken);

        var result = await response.Content.ReadFromJsonAsync<QueryVectorsResult>(cancellationToken: cancellationToken);

        return result;
    }

    public async Task UpsertAsync(UpsertDataModel inputModel, CancellationToken cancellationToken)
    {
        var headers = new Dictionary<string, string>
        {
            { "Api-Key", _pineconeOptions.ApiKey }
        };

        string content = JsonConvert.SerializeObject(inputModel);
        var buffer = Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var upsertEndpointUrl = $"{_pineconeOptions.IndexHostUrl}/vectors/upsert";

        await _httpProxy.PostAsync(new Uri(upsertEndpointUrl),
            headers,
            byteContent,
            cancellationToken);
    }
}