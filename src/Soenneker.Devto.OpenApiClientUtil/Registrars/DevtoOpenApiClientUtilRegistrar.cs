using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Devto.HttpClients.Registrars;
using Soenneker.Devto.OpenApiClientUtil.Abstract;

namespace Soenneker.Devto.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class DevtoOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="DevtoOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddDevtoOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddDevtoOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IDevtoOpenApiClientUtil, DevtoOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="DevtoOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddDevtoOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddDevtoOpenApiHttpClientAsSingleton()
                .TryAddScoped<IDevtoOpenApiClientUtil, DevtoOpenApiClientUtil>();

        return services;
    }
}

