namespace Sourcelyzer.Model.Analyzing
{
    public interface IAnalyzerResult
    {
        IRepository Repository { get; }
        
        string ToMarkdown();
    }
}