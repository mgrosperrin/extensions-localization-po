using System;
using MGR.Extensions.Localization.PortableObject;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension's methods for the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add localization using PortableObject files.
    /// </summary>
    /// <param name="services">The current <see cref="IServiceCollection"/>.</param>
    /// <param name="setupAction">n <see cref="T:System.Action`1" /> to configure the provided <see cref="T:PortableObjectLocalizationOptions" />.</param>
    /// <returns></returns>
    public static IServiceCollection AddPortableObjectLocalization(this IServiceCollection services,
        Action<PortableObjectLocalizationOptions> setupAction)
    {
        services.AddOptions();
        services.TryAddSingleton<IStringLocalizerFactory, PortableObjectStringLocalizerFactory>();
        services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        services.Configure(setupAction);
        return services;
    }
}
