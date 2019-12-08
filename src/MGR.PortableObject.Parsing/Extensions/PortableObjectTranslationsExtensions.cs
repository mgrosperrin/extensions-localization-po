// ReSharper disable once CheckNamespace
namespace MGR.PortableObject.Parsing
{
    /// <summary>
    ///  Extension's methods for <see cref="PortableObjectTranslations"/>.
    /// </summary>
    public static class PortableObjectTranslationsExtensions
    {
        /// <summary>
        /// Gets the translation for a given key (id + optional context).
        /// </summary>
        /// <param name="source">The current <see cref="PortableObjectTranslations"/>.</param>
        /// <param name="id">The Id for the translation.</param>
        /// <returns>The corresponding translation.</returns>
        public static PortableObjectTranslationItem GetTranslation(this PortableObjectTranslations source, string id) =>
            source.GetTranslation(new PortableObjectKey(id));

        /// <summary>
        /// Gets the translation for a given key (id + optional context).
        /// </summary>
        /// <param name="source">The current <see cref="PortableObjectTranslations"/>.</param>
        /// <param name="id">The Id for the translation.</param>
        /// <param name="context">The Context of the translation.</param>
        /// <returns>The corresponding translation.</returns>
        public static PortableObjectTranslationItem GetTranslation(this PortableObjectTranslations source, string id,
            string context) =>
            source.GetTranslation(new PortableObjectKey(id, context));
    }
}
