using System;

namespace Sourcelyzer.Collecting
{
    public class CollectingBuilder
    {
        public CollectingBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Builder { get; }

        public CollectingConfiguration Configuration { get; } = new CollectingConfiguration();

        public SourcelyzerBuilder Configure(Action<CollectingConfiguration> setupAction)
        {
            setupAction(Configuration);

            return Builder;
        }
    }
}
