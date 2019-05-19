using System;

namespace Sourcelyzer.GitHub.Collecting.Filter
{
    public class FilterBuilder
    {
        private GitHubCollectorBuilder Builder { get; } 
        
        private Options Options { get; }
        
        public FilterBuilder(GitHubCollectorBuilder builder, Options options)
        {
            Builder = builder;
            Options = options;
        }

        public GitHubCollectorBuilder Configure(Action<Options> setupAction)
        {
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            setupAction(Options);
            
            return Builder;
        }
    }
}