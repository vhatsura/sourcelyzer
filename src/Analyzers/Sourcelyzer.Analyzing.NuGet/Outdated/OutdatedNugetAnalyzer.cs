using System;
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

        public async Task<IAnalyzerResult> AnalyzeAsync(IRepository repository)
        {
            var files = (await repository.GetFilesAsync())
                .Where(f => f.Path.EndsWith(".csproj") || f.Path.EndsWith("packages.config"));

            return await GetAnalyzerResultsAsync(files, repository);
        }

        private async Task<IAnalyzerResult> GetAnalyzerResultsAsync(IEnumerable<IFile> files, IRepository repository)
        {
            var result = new OutdatedNuGetResult(repository);

            foreach (var file in files)
            {
                var packages = (await Reader.GetPackagesAsync(file))
                    .Select(x => GetNuGetMetadata(x))
                    .Where(x => x.IsPrerelease || x.IsOutdated)
                    .ToList();

                result.AddNuGetMetadata(packages, file);
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