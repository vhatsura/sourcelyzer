using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Sourcelyzer;
using Sourcelyzer.Analyzing.Nuget;
using Sourcelyzer.GitHub.Collecting;
using Sourcelyzer.Reporting.File;
using Sourcelyzer.Reporting.File.Options;

[assembly: ExcludeFromCodeCoverage]

namespace Example.NetCoreApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var sourcelyzer = new SourcelyzerBuilder()
                .Collecting.FromGitHub("Sourcelyzer",
                    builder => { })
                .Analyzing.FindOutdatedNuget(new[] {"https://api.nuget.org/v3/index.json"})
                .Reporting.AsHtmlFile("reports",
                    options => { })
                .Build();

            await sourcelyzer.RunAsync();
        }
    }
}
