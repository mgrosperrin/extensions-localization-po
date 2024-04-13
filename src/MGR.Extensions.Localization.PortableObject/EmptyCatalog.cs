using System.Globalization;
using MGR.PortableObject;

namespace MGR.Extensions.Localization.PortableObject;
internal class EmptyCatalog(CultureInfo culture) : ICatalog
{
    public CultureInfo Culture { get; } = culture;

    public int Count => 0;

    public IPortableObjectEntry GetEntry(PortableObjectKey key) => EmptyPortableObjectEntry.ForKey(key);
}
