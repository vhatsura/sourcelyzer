using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MoreLinq.Experimental;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Packaging;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace Sourcelyzer.Analyzing.NuGet.Client
{
    internal class NuGetClient : INuGetClient
    {
        private readonly ILogger _logger = new NullLogger();

        private readonly SourceCacheContext _sourceCacheContext = new SourceCacheContext();

        internal NuGetClient(IEnumerable<string> packageSources)
            : this(packageSources, new SourceRepositoryCreator())
        {
        }

        internal NuGetClient(IEnumerable<string> packageSources, ISourceRepositoryCreator creator)
        {
            SourceRepositories = packageSources
                                     ?.Select(creator.Create)
                                     .ToList() ?? throw new ArgumentNullException(nameof(packageSources));
        }

        private IDictionary<(string PackageSource, string PackageId), IList<NuGetVersion>> Cache { get; } =
            new Dictionary<(string PackageSource, string PackageId), IList<NuGetVersion>>();

        private IList<SourceRepository> SourceRepositories { get; }

        IEnumerable<(PackageSource PackageSource, IList<NuGetVersion> Versions)> INuGetClient.GetAllVersions(
            PackageReference package)
        {
            return GetAllVersions(package);
        }

        internal IEnumerable<(PackageSource PackageSource, IList<NuGetVersion> Versions)> GetAllVersions(
            PackageReference package)
        {
            return SourceRepositories
                .Await((repo, token) => GetVersionsFromPackageSource(repo, package.PackageIdentity.Id, token));
        }

        private async Task<(PackageSource PackageSource, IList<NuGetVersion> Versions)> GetVersionsFromPackageSource(
            SourceRepository repository, string packageId, CancellationToken token)
        {
            var packageSource = repository.PackageSource;

            if (Cache.TryGetValue((packageSource.Name, packageId), out var cachedVersions))
            {
                return (packageSource, cachedVersions);
            }

            var resource = await repository.GetResourceAsync<FindPackageByIdResource>(token);
            var versions = (resource != null
                    ? await resource.GetAllVersionsAsync(packageId, _sourceCacheContext, _logger, token)
                    : Enumerable.Empty<NuGetVersion>())
                .ToList();

            Cache.Add((packageSource.Name, packageId), versions);

            return (packageSource, versions);
        }
    }
}
