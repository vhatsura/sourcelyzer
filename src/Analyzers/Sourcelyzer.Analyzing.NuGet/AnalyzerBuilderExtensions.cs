using Sourcelyzer.Analyzing.Nuget.Outdated;

namespace Sourcelyzer.Analyzing.Nuget
{
    public static class AnalyzerBuilderExtensions
    {
        public static SourcelyzerBuilder FindOutdatedNuget(this AnalyzerBuilder builder, string[] packageSources)
        {
            var nugetOptions = new NugetOptions(packageSources);

            var analyzer = new OutdatedNugetAnalyzer(nugetOptions);

            builder.Options.AddAnalyzer(analyzer);

            return builder.Builder;
        }    
    }
}