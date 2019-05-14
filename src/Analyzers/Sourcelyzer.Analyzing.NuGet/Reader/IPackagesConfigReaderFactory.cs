using System.Xml.Linq;
using NuGet.Packaging;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    public interface IPackagesConfigReaderFactory
    {
        PackagesConfigReader CreateReader(XDocument document);
    }
}
