using System;
using System.Collections.Generic;
using System.Globalization;
using MGR.PortableObject;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MGR.Extensions.Localization.PortableObject;

internal class PortableObjectStringLocalizer : IStringLocalizer
{
    private readonly ILogger _logger;

    private readonly PortableObjectTranslationsProvider _portableObjectTranslationsProvider;
    private readonly string? _context;
    private readonly CultureInfo? _culture;

    public PortableObjectStringLocalizer(PortableObjectTranslationsProvider portableObjectTranslationsProvider, string? context, ILogger logger, CultureInfo? culture)
    {
        _portableObjectTranslationsProvider = portableObjectTranslationsProvider;
        _context = context;
        _logger = logger;
        _culture = culture;
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        throw new NotImplementedException();
    }

    public IStringLocalizer WithCulture(CultureInfo culture)
    {
        return new PortableObjectStringLocalizer(
                _portableObjectTranslationsProvider,
                _context,
                _logger,
                culture);
    }

    public LocalizedString this[string name]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            var translationItem = GetTranslationItem(name);

            return new LocalizedString(name, translationItem.GetTranslation(),
                resourceNotFound: !translationItem.HasTranslation, searchedLocation: "");
        }
    }

    private IPortableObjectEntry GetTranslationItem(string id)
    {
        var culture = GetCulture();
        var key = new PortableObjectKey(_context, id, null);
        var catalog = _portableObjectTranslationsProvider.GetCatalog(culture);
        var translationEntry = catalog.GetEntry(key);
        if (!translationEntry.HasTranslation)
        {
            translationEntry = catalog.GetEntry(new PortableObjectKey(id));
        }
        return translationEntry;
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            var translationItem = GetTranslationItem(name);


            return new LocalizedString(name, translationItem.GetTranslation(), resourceNotFound: !translationItem.HasTranslation, searchedLocation: "");
        }
    }

    private CultureInfo GetCulture()
    {
        return _culture ?? CultureInfo.CurrentUICulture;
    }
}
