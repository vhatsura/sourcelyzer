using System;
using System.Collections.Generic;
using System.Text;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Nuget.Outdated
{
    internal class OutdatedNuGetResult : IAnalyzerResult
    {
        internal OutdatedNuGetResult(IRepository repository, NuGetMetadata metadata, IEnumerable<string> projects)
        {
            if (metadata == null) throw new ArgumentNullException(nameof(metadata));

            if (!metadata.IsOutdated)
                throw new ArgumentException("The nuget package must be outdated", nameof(metadata));

            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            OutdatedNuGet = metadata;
            Projects = projects;
        }

        internal NuGetMetadata OutdatedNuGet { get; }

        internal IEnumerable<string> Projects { get; }

        public IRepository Repository { get; }

        public string Title =>
            $"Package {OutdatedNuGet.PackageName} need to be updated to {OutdatedNuGet.Latest} version";

        public string ToMarkdown()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"`{OutdatedNuGet.PackageName}` from ");
            stringBuilder.Append(
                $"[{OutdatedNuGet.PackageSource.SourceUri.Host}]({OutdatedNuGet.PackageSource.SourceUri}) ");
            stringBuilder.AppendLine("is outdated in:");

            foreach (var project in Projects)
            {
                stringBuilder.AppendLine($"* {project}");
            }

            stringBuilder.AppendLine();

            stringBuilder.AppendLine($"The latest version is {OutdatedNuGet.Latest}");

            return stringBuilder.ToString();
        }
    }
}
