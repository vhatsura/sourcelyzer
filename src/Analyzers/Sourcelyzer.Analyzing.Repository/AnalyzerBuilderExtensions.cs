using System;

namespace Sourcelyzer.Analyzing.Repository
{
    public static class AnalyzerBuilderExtensions
    {
        public static SourcelyzerBuilder ReadmeFileIsExist(this AnalyzingBuilder builder)
        {
            var analyzer = new ReadmeFileAnalyzer();
            
            builder.Configuration.Analyzers.Add(analyzer);
            
            return builder.Builder;
        }
    }
}