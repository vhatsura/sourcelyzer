using System;
using Octokit;
using Octokit.Internal;
using Sourcelyzer.GitHub.Collecting.Filter;

namespace Sourcelyzer.GitHub.Collecting
{
    public class CollectingBuilder
    {
        public FilterBuilder Filter { get; }

        private readonly Options _options;
        
        internal CollectingBuilder(string productName, Options options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            _options.ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            
            Filter = new FilterBuilder(this, _options.Filter);
        }
        
        public CollectingBuilder WithAuthorizationToken(string token)
        {
            _options.CredentialStore = new InMemoryCredentialStore(new Credentials(token)); 
            return this;
        }

        public CollectingBuilder UseCustomUri(Uri uri)
        {
            _options.GithubUri = uri ?? throw new ArgumentNullException(nameof(uri));
            return this;
        }
    }
}