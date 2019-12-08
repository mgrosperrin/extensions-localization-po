using System;
using System.Collections.Generic;
using System.Globalization;
using MGR.PortableObject.Parsing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace MGR.Extensions.Localization.POFile
{
    class PortableObjectStringLocalizer : IStringLocalizer
    {
        private readonly ILogger _logger;

        private readonly PortableObjectTranslationsProvider _portableObjectTranslationsProvider;
        private readonly string _context;
        private readonly CultureInfo _culture;

        public PortableObjectStringLocalizer(PortableObjectTranslationsProvider portableObjectTranslationsProvider, string context, ILogger logger, CultureInfo culture)
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
                    _culture);
        }

        public LocalizedString this[string name]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var translationItem = GetTranslationItem(name);

                return new LocalizedString(name, translationItem.GetTranslation(),
                    resourceNotFound: !translationItem.HasTranslation, searchedLocation: "");
            }
        }

        private PortableObjectTranslationItem GetTranslationItem(string id)
        {
            var culture = GetCulture();
            var key = new PortableObjectKey(id, _context);
            var translations = _portableObjectTranslationsProvider.GetTranslations(culture);
            var translationItem = translations.GetTranslation(key);
            if (!translationItem.HasTranslation)
            {
                translationItem = translations.GetTranslation(id);
            }
            return translationItem;
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var translationItem = GetTranslationItem(name);


                return new LocalizedString(name, translationItem.GetTranslation(), resourceNotFound: !translationItem.HasTranslation, searchedLocation: "");
            }
        }

        private CultureInfo GetCulture()
        {
            return _culture ?? CultureInfo.CurrentUICulture;
        }
    }
}
