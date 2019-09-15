using System.Collections.Generic;

namespace Sourcelyzer.Model.Analyzing
{
    public interface IAnalyzerResult
    {
        IRepository Repository { get; }

        string Title { get; }

        string ShortTitle { get; }

        IDictionary<string, string> TechnicalInfo { get; }

        string ToMarkdown();

        Status ActualizeStatus(IDictionary<string,string> technicalInfo);
    }
}
