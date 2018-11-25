using System.Collections.Generic;
using Sourcelyzer.Model;

namespace Sourcelyzer.Reporters
{
    public interface IReporter
    {
        void Report(IEnumerable<IAnalyzerResult> results);    
    }
}