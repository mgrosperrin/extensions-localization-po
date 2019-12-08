using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MGR.PortableObject.Parsing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MGR.Extensions.Localization.POFile
{
    internal class PortableObjectTranslationsProvider
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ConcurrentDictionary<string, PortableObjectTranslations> _cache = new ConcurrentDictionary<string, PortableObjectTranslations>(StringComparer.OrdinalIgnoreCase);
        private readonly IOptions<PortableObjectLocalizationOptions> _options;
        private readonly ILogger _logger;
        private readonly PortableObjectParser _portableObjectParser;

        public PortableObjectTranslationsProvider(IOptions<PortableObjectLocalizationOptions> options, IHostEnvironment hostEnvironment, ILogger logger)
        {
            _options = options;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
            _portableObjectParser = new PortableObjectParser();
        }

        public PortableObjectTranslations GetTranslations(CultureInfo culture)
        {
            var cultureName = culture.Name;
            var translations = _cache.GetOrAdd(cultureName, LoadTranslations);
            return translations;
        }

        private PortableObjectTranslations LoadTranslations(string cultureName)
        {
            var portableObjectFilePath = $"{_options.Value.ResourcesFolder}/{cultureName}.po";
            var portableObjectFile = _hostEnvironment.ContentRootFileProvider.GetFileInfo(portableObjectFilePath);
            if (portableObjectFile.Exists)
            {
                var translations = _portableObjectParser.Parse(new StreamReader(portableObjectFile.CreateReadStream()));
                return translations;
            }

            _logger.UnableToFindPortableObjectForCulture(cultureName);

            return new PortableObjectTranslations(new Dictionary<PortableObjectKey, string[]>());
        }
    }
}
