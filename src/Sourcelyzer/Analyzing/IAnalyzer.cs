using System.Collections.Generic;
using System.Threading.Tasks;
using Sourcelyzer.Model;

namespace Sourcelyzer.Analyzing
{
    public interface IAnalyzer
    {
        Task<IEnumerable<IAnalyzerResult>> AnalyzeAsync(IRepository repository);   
    }
}