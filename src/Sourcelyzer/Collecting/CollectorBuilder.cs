using System;

namespace Sourcelyzer.Collecting
{
    public class CollectorBuilder
    {
        public SourcelyzerBuilder Builder { get; }

        public CollectingOptions Options { get; } = new CollectingOptions();

        public CollectorBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Configure(Action<CollectingOptions> setupAction)
        {
            setupAction(Options);

            return Builder;
        }
    }
}