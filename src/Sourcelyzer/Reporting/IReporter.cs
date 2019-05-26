using System.Threading.Tasks;
using Sourcelyzer.Model.Analyzing;

namespace Sourcelyzer.Reporting
{
    public interface IReporter
    {
        Task ReportAsync(IAnalyzerResult result);
    }
}
