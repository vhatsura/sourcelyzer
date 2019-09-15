using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using NuGet.Packaging;
using Sourcelyzer.Model;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    internal class NuGetReferencesReader : INuGetReferencesReader
    {
        private readonly IPackageReferencesReaderFactory _factory;

        internal NuGetReferencesReader()
            : this(new PackageReferencesReaderFactory())
        {
        }

        internal NuGetReferencesReader(IPackageReferencesReaderFactory packageReferencesReaderFactory)
        {
            _factory = packageReferencesReaderFactory;
        }

        Task<IEnumerable<PackageReference>> INuGetReferencesReader.GetPackagesAsync(IFile file)
        {
            return GetPackagesAsync(file);
        }

        internal async Task<IEnumerable<PackageReference>> GetPackagesAsync(IFile file)
        {
            var content = await file.GetContentAsync();

            using (var stringReader = new StringReader(content.Trim('\uFEFF')))
            {
                var document = XDocument.Load(stringReader);

                var packageReferencesReader = _factory.CreateReader(file.Path);

                return packageReferencesReader.GetPackages(document);
            }
        }
    }
}
