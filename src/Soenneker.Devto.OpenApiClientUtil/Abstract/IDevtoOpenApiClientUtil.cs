using Soenneker.Devto.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Devto.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides the shared generated DEV.to v1 API client.
/// </summary>
public interface IDevtoOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>Gets the cached client configured with DEV.to authentication and API version headers.</summary>
    ValueTask<DevtoOpenApiClient> Get(CancellationToken cancellationToken = default);
}


