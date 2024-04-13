using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using MGR.PortableObject;
using MGR.PortableObject.Parsing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MGR.Extensions.Localization.PortableObject;

internal partial class PortableObjectTranslationsProvider
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ConcurrentDictionary<CultureInfo, ICatalog> _cache = new();
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

    public ICatalog GetCatalog(CultureInfo culture)
    {
        var catalog = _cache.GetOrAdd(culture, LoadCatalog);
        return catalog;
    }

    private ICatalog LoadCatalog(CultureInfo culture)
    {
        var portableObjectFilePath = $"{_options.Value.ResourcesFolder}/{culture.Name}.po";
        var portableObjectFile = _hostEnvironment.ContentRootFileProvider.GetFileInfo(portableObjectFilePath);
        if (portableObjectFile.Exists)
        {
            var parsingResultTask = _portableObjectParser.ParseAsync(new StreamReader(portableObjectFile.CreateReadStream()), culture);
            parsingResultTask.Wait();
            return parsingResultTask.Result.Catalog;
        }

        UnableToFindPortableObjectForCulture(culture.Name);

        return new EmptyCatalog(culture);
    }
    [LoggerMessage(EventId = 1000, Level = LogLevel.Error, Message = "Unable to find a Portable Object file for the culture '{culture}'")]
    private partial void UnableToFindPortableObjectForCulture(string culture);
}
