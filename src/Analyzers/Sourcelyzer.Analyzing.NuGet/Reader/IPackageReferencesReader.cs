using System.Collections.Generic;
using System.Xml.Linq;
using NuGet.Packaging;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    public interface IPackageReferencesReader
    {
        IEnumerable<PackageReference> GetPackages(XDocument document);
    }
}
