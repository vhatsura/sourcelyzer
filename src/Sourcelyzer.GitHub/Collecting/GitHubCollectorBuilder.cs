using System;
using Octokit;
using Octokit.Internal;
using Sourcelyzer.Collecting;
using Sourcelyzer.GitHub.Collecting.Filter;

namespace Sourcelyzer.GitHub.Collecting
{
    public class GitHubCollectorBuilder
    {
        private readonly Options _options;

        internal GitHubCollectorBuilder(string productName, Options options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

            _options.ProductName = productName ?? throw new ArgumentNullException(nameof(productName));

            Filter = new FilterBuilder(this, _options.Filter);
        }

        public FilterBuilder Filter { get; }

        public GitHubCollectorBuilder WithAuthorizationToken(string token)
        {
            _options.CredentialStore = new InMemoryCredentialStore(new Credentials(token, AuthenticationType.Bearer));
            return this;
        }

        public GitHubCollectorBuilder UseCustomUri(Uri uri)
        {
            _options.SetGitHubUri(uri);
            return this;
        }

        internal ICollector Build()
        {
            return new GitHubCollector(_options);
        }
    }
}
