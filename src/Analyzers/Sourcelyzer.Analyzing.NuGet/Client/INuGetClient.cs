using System.Collections.Generic;
using NuGet.Configuration;
using NuGet.Packaging;
using NuGet.Versioning;

namespace Sourcelyzer.Analyzing.NuGet.Client
{
    internal interface INuGetClient
    {
        IEnumerable<(PackageSource PackageSource, IList<NuGetVersion> Versions)> GetAllVersions(
            PackageReference package);
    }
}
