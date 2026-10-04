using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Devto.HttpClients.Abstract;
using Soenneker.Devto.OpenApiClientUtil.Abstract;
using Soenneker.Devto.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Devto.OpenApiClientUtil;
public sealed class DevtoOpenApiClientUtil : IDevtoOpenApiClientUtil
{
    private readonly AsyncSingleton<DevtoOpenApiClient> _client;

    public DevtoOpenApiClientUtil(IDevtoOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<DevtoOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Devto:ApiKey");
            string authHeaderName = configuration["Devto:AuthHeaderName"] ?? "api-key";
            string authHeaderValueTemplate = configuration["Devto:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue,
                additionalHeaders: new Dictionary<string, string> { ["Accept"] = "application/vnd.forem.api-v1+json" },
                allowedHosts: [httpClient.BaseAddress!.Host]),
                httpClient: httpClient) { BaseUrl = httpClient.BaseAddress!.AbsoluteUri.TrimEnd('/') };

            return new DevtoOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<DevtoOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}



