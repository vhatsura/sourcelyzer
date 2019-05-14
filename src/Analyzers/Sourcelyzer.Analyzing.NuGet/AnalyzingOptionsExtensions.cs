using Sourcelyzer.Analyzing.Nuget.Outdated;

namespace Sourcelyzer.Analyzing.Nuget
{
    public static class AnalyzingOptionsExtensions
    {
        public static void FindOutdatedNuget(this AnalyzingOptions options, string[] packageSources)
        {
            var nugetOptions = new NugetOptions(packageSources);

            var analyzer = new OutdatedNugetAnalyzer(nugetOptions);

            options.AddAnalyzer(analyzer);
        }
    }
}
