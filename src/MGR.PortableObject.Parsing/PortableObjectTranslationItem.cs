using System.Diagnostics;

namespace MGR.PortableObject.Parsing
{
    /// <summary>
    /// Represents a single translation from a PortableObject file.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public struct PortableObjectTranslationItem
    {
        /// <summary>
        /// Gets the key for the translation.
        /// </summary>
        public PortableObjectKey PortableObjectKey { get; }
        /// <summary>
        /// Gets the translation forms.
        /// </summary>
        public string[] Translations { get; }
        /// <summary>
        /// Indicates if the current translation has effective translations.
        /// </summary>
        public bool HasTranslation { get; }

        internal PortableObjectTranslationItem(PortableObjectKey portableObjectKey)
        {
            PortableObjectKey = portableObjectKey;
            Translations = new string[0];
            HasTranslation = false;
        }
        internal PortableObjectTranslationItem(PortableObjectKey portableObjectKey, string[] translations)
        {
            PortableObjectKey = portableObjectKey;
            Translations = translations;
            HasTranslation = true;
        }
        /// <summary>
        /// Gets the primary translation form.
        /// </summary>
        /// <remarks>If <seealso cref="HasTranslation"/> returns <code>false</code>, this method returns the Id of the Key.</remarks>
        /// <returns>A translation.</returns>
        public string GetTranslation()
        {
            return HasTranslation ? Translations[0] : PortableObjectKey.Id;
        }
        /// <summary>
        /// Gets the requested plural form of the translation.
        /// </summary>
        /// <param name="pluralNumber">The plural form number.</param>
        /// <remarks>If <seealso cref="HasTranslation"/> returns <code>false</code> or <code>pluralNumber</code> is greater than the number of translation forms, this method returns the Id of the Key.</remarks>
        /// <returns>A translation.</returns>
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
            // ReSharper disable once UnusedMember.Local
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