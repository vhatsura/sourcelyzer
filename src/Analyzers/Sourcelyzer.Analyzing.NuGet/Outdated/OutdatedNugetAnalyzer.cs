using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using NuGet.Configuration;
using NuGet.Packaging;
using Sourcelyzer.Analyzing.NuGet.Client;
using Sourcelyzer.Analyzing.NuGet.Reader;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Nuget.Outdated
{
    public class OutdatedNugetAnalyzer : IAnalyzer
    {
        internal OutdatedNugetAnalyzer(NugetOptions options)
            : this(new NuGetReferencesReader(), new NuGetClient(options?.PackageSources))
        {
        }

        internal OutdatedNugetAnalyzer(INuGetReferencesReader reader, INuGetClient nuGetClient)
        {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            NuGetClient = nuGetClient ?? throw new ArgumentNullException(nameof(nuGetClient));
        }

        private INuGetReferencesReader Reader { get; }

        private INuGetClient NuGetClient { get; }

        public async Task<IEnumerable<IAnalyzerResult>> AnalyzeAsync(IRepository repository)
        {
            var files = (await repository.GetFilesAsync())
                .Where(f => f.Path.EndsWith(".csproj") || f.Path.EndsWith("packages.config"));

            var outDatedNuGets = await GetAnalyzerResultsAsync(files);

            return outDatedNuGets.SelectMany(x => x.OutDatedNuGets.Select(n => (x.Project, NuGetMetadata: n)))
                .GroupBy(x => x.NuGetMetadata.PackageName)
                .Select(x => new OutdatedNuGetResult(repository, x.First().NuGetMetadata, x.Select(v => v.Project)))
                .ToList();
        }

        private async Task<IEnumerable<(string Project, IEnumerable<NuGetMetadata> OutDatedNuGets)>>
            GetAnalyzerResultsAsync(IEnumerable<IFile> files)
        {
            var result = new ConcurrentBag<(string, IEnumerable<NuGetMetadata>)>();

            foreach (var file in files.AsParallel())
            {
                var packages = (await Reader.GetPackagesAsync(file))
                    .Select(x => GetNuGetMetadata(x))
                    .Where(x => x.IsPrerelease || x.IsOutdated)
                    .ToList();

                if (packages.Any()) result.Add((file.Path, packages));
            }

            return result;
        }

        private NuGetMetadata GetNuGetMetadata(PackageReference package)
        {
            try
            {
                var packageVersions = NuGetClient.GetAllVersions(package);

                var latest = packageVersions
                    .Select(x => (
                        x.PackageSource,
                        Version: x.Versions.Where(v => !v.IsPrerelease).DefaultIfEmpty().Max(v => v?.Version)))
                    .MaxBy(x => x.Version)
                    .FirstOrDefault(x => x.Version != default);

                return new NuGetMetadata(package, latest);
            }
            catch (Exception exception)
            {
                return new NuGetMetadata(package,
                    (PackageSource: new PackageSource("local"), package.PackageIdentity.Version.Version));
            }
        }
    }
}
