using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MGR.Extensions.Localization.PortableObject
{
    internal class PortableObjectStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ILogger _logger;
        private PortableObjectTranslationsProvider _portableObjectTranslationsProvider;

        public PortableObjectStringLocalizerFactory(IOptions<PortableObjectLocalizationOptions> localizationOptions, IHostEnvironment hostEnvironment, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PortableObjectStringLocalizer>();
            _portableObjectTranslationsProvider = new PortableObjectTranslationsProvider(localizationOptions, hostEnvironment, _logger);
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new PortableObjectStringLocalizer(_portableObjectTranslationsProvider, resourceSource.FullName, _logger, null);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new PortableObjectStringLocalizer(_portableObjectTranslationsProvider, location + "." + baseName, _logger, null);
        }
    }
}