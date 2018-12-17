using System.Collections.Generic;
using System.Threading.Tasks;
using Sourcelyzer.Model;

namespace Sourcelyzer.Analyzing
{
    public interface IAnalyzer
    {
        Task<IAnalyzerResult> AnalyzeAsync(IRepository repository);
    }
}