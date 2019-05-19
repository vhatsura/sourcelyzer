using System;

namespace Sourcelyzer.Collecting
{
    public class CollectingBuilder
    {
        public SourcelyzerBuilder Builder { get; }

        public CollectingConfiguration Configuration { get; } = new CollectingConfiguration();

        public CollectingBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Configure(Action<CollectingConfiguration> setupAction)
        {
            setupAction(Configuration);

            return Builder;
        }
    }
}