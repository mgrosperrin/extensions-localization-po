using System.Collections.Generic;

namespace MGR.PortableObject.Parsing
{
    /// <summary>
    /// Represents the translations for a culture from a PortableObject file.
    /// </summary>
    public class PortableObjectTranslations
    {
        private readonly Dictionary<PortableObjectKey, string[]> _translations;

        /// <summary>
        /// Creates a new instance of <see cref="PortableObjectTranslations"/>.
        /// </summary>
        /// <param name="translations">The translations found in the PortableObject file.</param>
        public PortableObjectTranslations(Dictionary<PortableObjectKey, string[]> translations)
        {
            _translations = translations;
        }
        /// <summary>
        /// The number of translations found in the file.
        /// </summary>
        public int Count => _translations.Count;

        /// <summary>
        /// Gets the translation for a given key (id + optional context).
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The corresponding translation.</returns>
        public PortableObjectTranslationItem GetTranslation(PortableObjectKey key)
        {
            if (_translations.ContainsKey(key))
            {
                return new PortableObjectTranslationItem(key, _translations[key]);
            }
            return new PortableObjectTranslationItem(key);
        }
    }
}