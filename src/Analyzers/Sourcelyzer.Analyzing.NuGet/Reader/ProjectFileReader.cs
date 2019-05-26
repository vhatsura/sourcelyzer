using System.Collections.Generic;
using System.Xml.Linq;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Versioning;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    public class ProjectFileReader : IPackageReferencesReader
    {
        public IEnumerable<PackageReference> GetPackages(XDocument document)
        {
            if (document.Root == null || !document.Root.HasElements) yield break;

            foreach (var element in document.Root.Descendants("PackageReference"))
            {
                var packageId = element.Attribute("Include")?.Value;
                var version = element.Attribute("Version")?.Value;

                if (packageId != null && version != null)
                    yield return new PackageReference(new PackageIdentity(packageId, NuGetVersion.Parse(version)),
                        NuGetFramework.AgnosticFramework);
            }
        }
    }
}
