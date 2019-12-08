using System.Collections.Generic;

namespace MGR.PortableObject.Parsing
{
    public class PortableObjectTranslations
    {
        private readonly Dictionary<PortableObjectKey, string[]> _translations;

        public PortableObjectTranslations(Dictionary<PortableObjectKey, string[]> translations)
        {
            _translations = translations;
        }

        public int Count => _translations.Count;

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