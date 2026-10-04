using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Devto.OpenApiClientUtil.Tests;

internal sealed class RecordingHandler : HttpMessageHandler
{
    public Uri? RequestUri { get; private set; }
    public string? ApiKey { get; private set; }
    public string? Accept { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        RequestUri = request.RequestUri;
        ApiKey = request.Headers.GetValues("api-key").Single();
        Accept = request.Headers.Accept.ToString();
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("[{\"id\":1,\"title\":\"Test article\"}]", Encoding.UTF8, "application/json")
        });
    }
}
