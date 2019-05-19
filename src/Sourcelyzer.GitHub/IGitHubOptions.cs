using Octokit;

namespace Sourcelyzer.GitHub
{
    internal interface IGitHubOptions
    {
        GitHubClient Client { get; }
    }
}
