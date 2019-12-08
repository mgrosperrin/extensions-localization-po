using System;
using MGR.Extensions.Localization.PortableObject;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
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
}
