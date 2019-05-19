using System;
using Sourcelyzer.Collecting;

namespace Sourcelyzer.GitHub.Collecting
{
    public static class CollectorBuilderExtensions
    {
        public static SourcelyzerBuilder FromGitHub(this CollectingBuilder builder, string productName,
            Action<GitHubCollectorBuilder> configureAction = null)
        {
            var collectingBuilder = new GitHubCollectorBuilder(productName, Options.Create());
            
            configureAction?.Invoke(collectingBuilder);
            
            builder.Configuration.AddCollector(collectingBuilder.Build());
            
            return builder.Builder;
        }
    }
}