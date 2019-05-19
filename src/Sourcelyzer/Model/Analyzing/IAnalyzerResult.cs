namespace Sourcelyzer.Model.Analyzing
{
    public interface IAnalyzerResult
    {
        IRepository Repository { get; }
        
        string Title { get; }
        
        string ToMarkdown();
    }
}