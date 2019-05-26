using System;
using System.Collections.Generic;
using System.Text;
using NuGet.Configuration;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Nuget.Outdated
{
    internal class OutdatedNuGetResult : IAnalyzerResult
    {
        internal OutdatedNuGetResult(IRepository repository, string packageName, PackageSource source, Version latest,
            IEnumerable<(string Project, Version Version)> projects)
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

        internal Version Latest { get; }

        internal PackageSource PackageSource { get; }

        internal IEnumerable<(string Project, Version Version)> Projects { get; }

        public IRepository Repository { get; }

        public string Title =>
            $"Package {PackageName} need to be updated to {Latest} version";

        public string ShortTitle => PackageName;

        public string ToMarkdown()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"`{PackageName}` from ");
            stringBuilder.Append(
                $"[{PackageSource.SourceUri.Host}]({PackageSource.SourceUri}) ");
            stringBuilder.AppendLine("is outdated in:");

            foreach (var project in Projects)
            {
                stringBuilder.AppendLine($"* {project.Project} - {project.Version}");
            }

            stringBuilder.AppendLine();

            stringBuilder.AppendLine($"The latest version is {Latest}");

            return stringBuilder.ToString();
        }
    }
}
