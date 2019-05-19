using System.Collections.Generic;
using System.Threading.Tasks;
using Sourcelyzer.Model;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Reporting
{
    public interface IReporter
    {
        Task ReportAsync(IAnalyzerResult result);    
    }
}