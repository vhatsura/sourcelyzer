using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing.Repository
{
    public class ReadmeFileAnalyzer : BaseAnalyzer
    {
        protected override IEnumerable<string> FilesToAnalysis => new List<string>{ "readme.md" };
        protected override Task<IEnumerable<IAnalyzerResult>> AnalyzeAsync(IEnumerable<IFile> files, IRepository repository)
        {
            if (!files.Any(x => x.Path.Equals("readme.md", StringComparison.OrdinalIgnoreCase)))
            {
                return Task.FromResult(((IAnalyzerResult)new MissedReadmeFile(repository)).Yield());
            }

            return Task.FromResult(Enumerable.Empty<IAnalyzerResult>());
        }
    }
}