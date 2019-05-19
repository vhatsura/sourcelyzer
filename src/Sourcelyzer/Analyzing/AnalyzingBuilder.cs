using System;

namespace Sourcelyzer.Analyzing
{
    public class AnalyzingBuilder
    {
        public SourcelyzerBuilder Builder { get; }

        public AnalyzingConfiguration Configuration { get; } = new AnalyzingConfiguration();

        public AnalyzingBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Configure(Action<AnalyzingConfiguration> setupAction)
        {
            setupAction(Configuration);

            return Builder;
        }
    }
}