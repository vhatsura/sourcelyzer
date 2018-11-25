using System;

namespace Sourcelyzer.Analyzers
{
    public class AnalyzerBuilder
    {
        public SourcelyzerBuilder Builder { get; }
        
        public AnalyzersOptions Options { get; } = new AnalyzersOptions();

        public AnalyzerBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Configure(Action<AnalyzersOptions> setupAction)
        {
            setupAction(Options);
            
            return Builder;
        }
    }
}