using Sourcelyzer.Analyzing.Nuget.Config;
using Sourcelyzer.Analyzing.Nuget.Outdated;

namespace Sourcelyzer.Analyzing.Nuget
{
    public static class AnalyzerBuilderExtensions
    {
        public static SourcelyzerBuilder FindOutdatedNuget(this AnalyzingBuilder builder, string[] packageSources)
        {
            var nugetOptions = new NugetOptions(packageSources);

            var analyzer = new OutdatedNugetAnalyzer(nugetOptions);

            builder.Configuration.AddAnalyzer(analyzer);

            return builder.Builder;
        }

        public static SourcelyzerBuilder NuGetConfigIsPresented(this AnalyzingBuilder builder)
        {
            var analyzer = new NuGetConfigAnalyzer();
            
            builder.Configuration.AddAnalyzer(analyzer);
            
            return builder.Builder;
        }
    }
}
