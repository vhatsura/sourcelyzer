using System;
using System.Xml.Linq;
using NuGet.Packaging;

namespace Sourcelyzer.Analyzing.NuGet.Reader
{
    public class PackageReferencesReaderFactory : IPackageReferencesReaderFactory
    {
        private readonly PackagesConfigReader _packagesConfigReader = new PackagesConfigReader();
        private readonly ProjectFileReader _projectFileReader = new ProjectFileReader();
        
        public IPackageReferencesReader CreateReader(string path)
        {
            if (path.EndsWith("packages.config"))
            {
                return _packagesConfigReader;    
            }

            if (path.EndsWith(".csproj"))
            {
                return _projectFileReader;
            }

            throw new InvalidOperationException($"Unable to get nuget packages from {path}");
        }
    }
}
