using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NuGet.Packaging;
using Sourcelyzer.Model;

[assembly: InternalsVisibleTo("Sourcelyzer.Analyzing.NuGet.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    internal interface INuGetReferencesReader
    {
        Task<IEnumerable<PackageReference>> GetPackagesAsync(IFile file);
    }
}
