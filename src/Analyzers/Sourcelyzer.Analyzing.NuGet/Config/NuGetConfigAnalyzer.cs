using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NuGet.Packaging;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Nuget.Config
{
    public class NuGetConfigAnalyzer : BaseAnalyzer
    {
        protected override IEnumerable<string> FilesToAnalysis => new List<string>
        {
            ".sln",
            "nuget.config"
        };

        protected override async Task<IEnumerable<IAnalyzerResult>> AnalyzeAsync(IEnumerable<IFile> files, IRepository repository)
        {
            //todo: handle a few solution files in repo
            var solutionFile = files.FirstOrDefault(f => f.Path.EndsWith(".sln"));

            if (solutionFile != null)
            {
                var nugetConfigs = files
                    .Where(f => f.Path.EndsWith("nuget.config", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (!nugetConfigs.Any())
                {
                    return new MissedNuGetConfig(repository).Yield();
                }

                if (nugetConfigs.Count == 1)
                {
                    var nugetConfigPath = DirectoryPath(nugetConfigs.Single().Path);
                    var solutionPath = DirectoryPath(solutionFile.Path);

                    if (nugetConfigPath != solutionPath)
                    {
                        var relativePathToSolutionFile = solutionPath == string.Empty
                            ? nugetConfigPath
                            : nugetConfigPath.Replace(solutionPath, string.Empty);

                        var nugetFolders = new HashSet<string> {"/.nuget", ".nuget"};
                        if (!nugetFolders.Contains(relativePathToSolutionFile) && !solutionPath.StartsWith(nugetConfigPath))
                        {
                            return new InvalidNuGetConfigLocation(repository, nugetConfigPath, solutionPath).Yield();
                        }
                    }
                }
                else
                {
                    int i = 0;
                }
            }

            return Enumerable.Empty<IAnalyzerResult>();

            string DirectoryPath(string path)
            {
                var lastIndex = path.LastIndexOf('/');
                return lastIndex > 0 ? path.Substring(0, lastIndex) : "";
            }
        }
    }
}
