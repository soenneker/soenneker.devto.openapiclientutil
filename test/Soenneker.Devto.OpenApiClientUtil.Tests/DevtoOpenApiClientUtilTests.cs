using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace Soenneker.Devto.OpenApiClientUtil.Tests;

public sealed class DevtoOpenApiClientUtilTests
{
    [Test]
    public async Task Get_sends_v1_headers_and_deserializes_articles(CancellationToken cancellationToken)
    {
        using var handler = new RecordingHandler();
        using var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://dev.to/") };
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Devto:ApiKey"] = "test-api-key"
        }).Build();
        await using var utility = new DevtoOpenApiClientUtil(new TestHttpClient(httpClient), configuration);
        var client = await utility.Get(cancellationToken: cancellationToken);
        var articles = await client.Api.Articles.GetAsync(options => options.QueryParameters.PerPage = 1, cancellationToken: cancellationToken);
        await Assert.That(handler.RequestUri!.AbsolutePath).IsEqualTo("/api/articles");
        await Assert.That(handler.RequestUri.Query).Contains("per_page=1");
        await Assert.That(handler.ApiKey).IsEqualTo("test-api-key");
        await Assert.That(handler.Accept).IsEqualTo("application/vnd.forem.api-v1+json");
        await Assert.That(articles![0].Title).IsEqualTo("Test article");
        await Assert.That(ReferenceEquals(client, await utility.Get(cancellationToken: cancellationToken))).IsTrue();
    }
}
