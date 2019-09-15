using System;

namespace Sourcelyzer.Analyzing
{
    public class AnalyzingBuilder
    {
        public AnalyzingBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Builder { get; }

        public AnalyzingConfiguration Configuration { get; } = new AnalyzingConfiguration();

        public SourcelyzerBuilder Configure(Action<AnalyzingConfiguration> setupAction)
        {
            setupAction(Configuration);

            return Builder;
        }
    }
}
