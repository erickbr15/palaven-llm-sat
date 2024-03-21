namespace Liara.Common.Http;

public interface IHttpProxy
{
    Task<HttpResponseMessage> GetAsync(Uri uri, IDictionary<string, string> headers, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostAsync(Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken);
}
