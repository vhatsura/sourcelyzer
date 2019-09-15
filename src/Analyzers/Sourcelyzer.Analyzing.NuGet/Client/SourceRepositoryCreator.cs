using System;
using System.Collections.Generic;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;

namespace Sourcelyzer.Analyzing.NuGet.Client
{
    internal class SourceRepositoryCreator : ISourceRepositoryCreator
    {
        public SourceRepositoryCreator()
        {
            Providers = Repository.Provider.GetCoreV3();
        }

        private IEnumerable<Lazy<INuGetResourceProvider>> Providers { get; }

        public SourceRepository Create(string packageSource)
        {
            if (string.IsNullOrWhiteSpace(packageSource)) throw new ArgumentNullException(nameof(packageSource));

            var source = new PackageSource(packageSource);

            return new SourceRepository(source, Providers);
        }
    }
}
