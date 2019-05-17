using System;
using Sourcelyzer.Collecting;

namespace Sourcelyzer.GitHub.Collecting
{
    public static class CollectorBuilderExtensions
    {
        public static SourcelyzerBuilder FromGitHub(this CollectorBuilder builder, string productName,
            Action<CollectingBuilder> configureAction = null)
        {
            var githubOptions = new Options();

            var collector = new GithubCollector(githubOptions);

            builder.Options.AddCollector(collector);

            var collectingBuilder = new CollectingBuilder(productName, githubOptions);
            
            configureAction?.Invoke(collectingBuilder);
            
            return builder.Builder;
        }
    }
}