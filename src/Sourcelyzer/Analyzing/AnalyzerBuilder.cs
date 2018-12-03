using System;

namespace Sourcelyzer.Analyzing
{
    public class AnalyzerBuilder
    {
        public SourcelyzerBuilder Builder { get; }
        
        public AnalyzingOptions Options { get; } = new AnalyzingOptions();

        public AnalyzerBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Configure(Action<AnalyzingOptions> setupAction)
        {
            setupAction(Options);
            
            return Builder;
        }
    }
}