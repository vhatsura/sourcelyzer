using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Analyzing
{
    public abstract class BaseAnalyzer : IAnalyzer
    {
        protected abstract IEnumerable<string> FilesToAnalysis { get; }

        public async Task<IEnumerable<IAnalyzerResult>> AnalyzeAsync(IRepository repository)
        {
            var files = (await repository.GetFilesAsync()).Where(x =>
                FilesToAnalysis.Any(f => x.Path.EndsWith(f, StringComparison.OrdinalIgnoreCase)));

            return await AnalyzeAsync(files, repository);
        }

        protected abstract Task<IEnumerable<IAnalyzerResult>> AnalyzeAsync(IEnumerable<IFile> files,
            IRepository repository);
    }
}