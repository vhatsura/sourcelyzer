using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NuGet.Configuration;
using NuGet.Versioning;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Nuget.Outdated
{
    [DebuggerDisplay("{PackageName}:{Latest}")]
    internal class OutdatedNuGet : IAnalyzerResult
    {
        internal OutdatedNuGet(IRepository repository, string packageName, PackageSource source, NuGetVersion latest,
            IEnumerable<(string Project, NuGetVersion Version)> projects)
        {
            if (string.IsNullOrWhiteSpace(packageName))
                throw new ArgumentNullException(nameof(packageName));

            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            PackageSource = source ?? throw new ArgumentNullException(nameof(source));
            Latest = latest ?? throw new ArgumentNullException(nameof(latest));

            Projects = projects;
            PackageName = packageName;
        }

        internal string PackageName { get; }

        internal NuGetVersion Latest { get; }

        internal PackageSource PackageSource { get; }

        internal IEnumerable<(string Project, NuGetVersion Version)> Projects { get; }

        public IRepository Repository { get; }

        public string Title =>
            $"Package {PackageName} need to be updated to {Latest} version";

        public string ShortTitle => PackageName;

        public IDictionary<string, string> TechnicalInfo => new Dictionary<string, string>
        {
            {nameof(PackageName), PackageName},
            {nameof(Latest), Latest.ToFullString()}
        };

        public string ToMarkdown()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"`{PackageName}` from ");
            stringBuilder.Append(
                PackageSource.TrySourceAsUri != null
                    ? $"[{PackageSource.SourceUri.Host}]({PackageSource.SourceUri}) "
                    : $"`{PackageSource.Name}` ");

            stringBuilder.AppendLine("is outdated in:");

            foreach (var (project, version) in Projects)
            {
                stringBuilder.AppendLine($"* {project} - {version}");
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"The latest version is {Latest}");

            return stringBuilder.ToString();
        }

        public Status ActualizeStatus(IDictionary<string, string> technicalInfo)
        {
            if (!technicalInfo.ContainsKey(nameof(PackageName))) return Status.Another;

            if (!technicalInfo[nameof(PackageName)].Equals(PackageName, StringComparison.OrdinalIgnoreCase))
                return Status.Another;

            var reportedVersion = NuGetVersion.Parse(technicalInfo[nameof(Latest)]);

            return Latest > reportedVersion ? Status.NeedToUpdate : Status.UpToDate;
        }
    }
}
