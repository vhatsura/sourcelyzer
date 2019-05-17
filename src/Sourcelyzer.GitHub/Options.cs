using System;
using Octokit;

namespace Sourcelyzer.GitHub
{
    internal class Options
    {
        internal ICredentialStore CredentialStore { private get; set; }

        internal Uri GithubUri { private get; set; } = new Uri("https://github.com");

        internal GitHubClient Client => new GitHubClient(new ProductHeaderValue(ProductName), CredentialStore, GithubUri);

        internal string ProductName { private get; set; }
        
        internal Collecting.Filter.Options Filter { get; } = new Collecting.Filter.Options();
    }
}