using System.Xml.Linq;
using NuGet.Packaging;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    public class PackagesConfigReaderFactory : IPackagesConfigReaderFactory
    {
        public PackagesConfigReader CreateReader(XDocument document)
        {
            return new PackagesConfigReader(document);
        }
    }
}
