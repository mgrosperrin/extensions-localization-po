namespace MGR.Extensions.Localization.PortableObject
{
    /// <summary>
    /// Provides options for the localization based on PortableObject files.
    /// </summary>
    public class PortableObjectLocalizationOptions
    {
        /// <summary>
        /// Sets the folder containing the translations resources.
        /// </summary>
        /// <param name="resourcesFolder">The resources folder.</param>
        /// <returns>This instance of <see cref="PortableObjectLocalizationOptions"/> with the resources folder configured.</returns>
        public PortableObjectLocalizationOptions SetResourcesFolder(string resourcesFolder)
        {
            ResourcesFolder = resourcesFolder;
            return this;
        }

        internal string ResourcesFolder { get; private set; } = "Resources";
    }
}