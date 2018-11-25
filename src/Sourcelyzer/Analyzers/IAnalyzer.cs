using System.Collections.Generic;
using System.Threading.Tasks;
using Sourcelyzer.Model;

namespace Sourcelyzer.Analyzers
{
    public interface IAnalyzer
    {
        Task<IEnumerable<IAnalyzerResult>> AnalyzeAsync(IRepository repository);   
    }
}