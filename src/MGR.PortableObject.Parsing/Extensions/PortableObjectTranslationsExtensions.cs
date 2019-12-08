// ReSharper disable once CheckNamespace
namespace MGR.PortableObject.Parsing
{
    public static class PortableObjectTranslationsExtensions
    {
        public static PortableObjectTranslationItem GetTranslation(this PortableObjectTranslations source, string id) =>
            source.GetTranslation(new PortableObjectKey(id));

        public static PortableObjectTranslationItem GetTranslation(this PortableObjectTranslations source, string id,
            string context) =>
            source.GetTranslation(new PortableObjectKey(id, context));
    }
}
