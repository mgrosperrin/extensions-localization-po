using System;
using System.Diagnostics;

namespace MGR.PortableObject.Parsing
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public struct PortableObjectTranslationItem
    {
        public PortableObjectKey PortableObjectKey { get; }
        public string[] Translations { get; }
        public bool HasTranslation { get; }
        public PortableObjectTranslationItem(PortableObjectKey portableObjectKey)
        {
            PortableObjectKey = portableObjectKey;
            Translations = new string[0];
            HasTranslation = false;
        }
        public PortableObjectTranslationItem(PortableObjectKey portableObjectKey, string[] translations)
        {
            PortableObjectKey = portableObjectKey;
            Translations = translations;
            HasTranslation = true;
        }

        public string GetTranslation()
        {
            return HasTranslation ? Translations[0] : PortableObjectKey.Id;
        }

        public string GetPluralTranslation(int pluralNumber)
        {
            if (HasTranslation && Translations.Length >= pluralNumber)
            {
                return Translations[pluralNumber];
            }

            return PortableObjectKey.Id;
        }

        private string DebuggerDisplay
        {
            get
            {
                var debugTranslations = HasTranslation ?
                    Translations[0] + (Translations.Length > 1 ?
                        $" (+ {Translations.Length} plural forms)"
                        : "")
                    : "no translations";

                return $"{PortableObjectKey}: {debugTranslations}";
            }
        }
    }
}