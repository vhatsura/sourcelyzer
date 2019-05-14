using System.Collections.Generic;
using System.Text;
using Sourcelyzer.Model;

namespace Sourcelyzer.Analyzing.Nuget.Outdated
{
    public class OutdatedNuGetResult : IAnalyzerResult
    {
        internal IDictionary<string, IEnumerable<NuGetMetadata>> OutdatedNuGets { get; } =
            new Dictionary<string, IEnumerable<NuGetMetadata>>();

        public string ToMarkdown()
        {
            var stringBuilder = new StringBuilder();

            foreach (var project in OutdatedNuGets)
            {
                stringBuilder.AppendLine($"In `{project.Key}` founded the following outdated nuget packages:");
                
                foreach (var nuGetMetadata in project.Value)
                {
                    stringBuilder.Append($"- [ ] `{nuGetMetadata.PackageName}` from ");
                    stringBuilder.Append(
                        $"[{nuGetMetadata.PackageSource.Name}]({nuGetMetadata.PackageSource.Source}) can be updated from ");
                    stringBuilder.AppendLine($"{nuGetMetadata.Current} to {nuGetMetadata.Latest}");
                }
            }

            return stringBuilder.ToString();
        }

        internal void AddNuGetMetadata(IEnumerable<NuGetMetadata> metadata, IFile file)
        {
            OutdatedNuGets.Add(file.Path, metadata);
        }
    }
}
