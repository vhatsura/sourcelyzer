using System;

namespace Sourcelyzer.GitHub.Collecting.Filter
{
    public class FilterBuilder
    {
        private CollectingBuilder Builder { get; } 
        
        private Options Options { get; }
        
        public FilterBuilder(CollectingBuilder builder, Options options)
        {
            Builder = builder;
            Options = options;
        }

        public CollectingBuilder Configure(Action<Options> setupAction)
        {
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            setupAction(Options);
            
            return Builder;
        }
    }
}