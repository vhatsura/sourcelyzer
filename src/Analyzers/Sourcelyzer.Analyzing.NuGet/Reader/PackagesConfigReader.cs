using System.Collections.Generic;
using System.Xml.Linq;
using NuGet.Packaging;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    public class PackagesConfigReader : IPackageReferencesReader
    {
        public IEnumerable<PackageReference> GetPackages(XDocument document)
        {
            return new global::NuGet.Packaging.PackagesConfigReader(document).GetPackages();
        }
    }
}
