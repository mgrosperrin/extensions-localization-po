using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Logging
{
    internal static class LoggerExtensions
    {
        private static readonly Action<ILogger, string, Exception> UnableToFindPortableObjectForCultureAction = LoggerMessage.Define<string>(LogLevel.Error, new EventId(1000, "PortableObjectStringLocalizer.1000"), "Unable to find a Portable Object file for the culture '{culture}'");

        public static void UnableToFindPortableObjectForCulture(this ILogger logger, string cultureName)
        {
            UnableToFindPortableObjectForCultureAction(logger, cultureName, null);
        }
    }
}
