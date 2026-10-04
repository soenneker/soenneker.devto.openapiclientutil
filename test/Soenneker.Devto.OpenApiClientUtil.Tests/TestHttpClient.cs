using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Devto.HttpClients.Abstract;

namespace Soenneker.Devto.OpenApiClientUtil.Tests;

internal sealed class TestHttpClient(HttpClient client) : IDevtoOpenApiHttpClient
{
    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default) => ValueTask.FromResult(client);
    public void Dispose() { }
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
