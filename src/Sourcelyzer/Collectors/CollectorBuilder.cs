using System;

namespace Sourcelyzer.Collectors
{
    public class CollectorBuilder
    {
        public SourcelyzerBuilder Builder { get; }

        public CollectorsOptions Options { get; } = new CollectorsOptions();
        
        public CollectorBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Configure(Action<CollectorsOptions> setupAction)
        {
            setupAction(Options);
            
            return Builder;
        }    
    }
}