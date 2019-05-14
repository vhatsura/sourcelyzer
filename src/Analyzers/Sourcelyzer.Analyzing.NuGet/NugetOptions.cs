using System.Collections.Generic;

namespace Sourcelyzer.Analyzing.Nuget
{
    public class NugetOptions
    {
        public NugetOptions(string[] packageSources)
        {
            PackageSources = new List<string>(packageSources);
        }

        internal IList<string> PackageSources { get; }
    }
}
