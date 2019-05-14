using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using NuGet.Packaging;
using Sourcelyzer.Model;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    internal class NuGetReferencesReader : INuGetReferencesReader
    {
        private readonly IPackagesConfigReaderFactory _factory;
        
        internal NuGetReferencesReader()
            : this(new PackagesConfigReaderFactory())
        {
        }

        internal NuGetReferencesReader(IPackagesConfigReaderFactory packagesConfigReaderFactory)
        {
            _factory = packagesConfigReaderFactory;
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
                var xDocument = XDocument.Load(stringReader);

                if (file.Path.EndsWith("packages.config"))
                {
                    var reader = _factory.CreateReader(xDocument);
                    return reader?.GetPackages() ?? Enumerable.Empty<PackageReference>();
                }

                if (file.Path.EndsWith(".csproj"))
                {
                    return Enumerable.Empty<PackageReference>();
                }

                throw new InvalidOperationException($"Unable to get nuget packages from {file.Path}");
            }
        }
    }
}
