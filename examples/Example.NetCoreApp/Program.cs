using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Sourcelyzer;
using Sourcelyzer.Analyzing.Nuget;
using Sourcelyzer.GitHub.Collecting;

namespace Example.NetCoreApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sourcelyzer = new SourcelyzerBuilder()
                .Collecting.FromGitHub("Sourcelyzer",
                    builder =>
                    {
                        
                    })
                .Analyzing.FindOutdatedNuget(new[] {"https://api.nuget.org/v3/index.json"})
                .Build();

            await sourcelyzer.RunAsync();
        }
    }
}