using System.Collections.Generic;
using Sourcelyzer.Model;

namespace Sourcelyzer.Reporting
{
    public interface IReporter
    {
        void Report(IAnalyzerResult result);    
    }
}