using Octokit;
using Sourcelyzer.GitHub.Models;
using Sourcelyzer.Model;
using Repository = Sourcelyzer.GitHub.Models.Repository;

namespace Sourcelyzer.GitHub.Extensions
{
    public static class RepositoryExtensions
    {
        public static IRepository Transform(this Octokit.Repository repository, GitHubClient client)
        {
            return new Repository(repository, client);   
        }

        public static IFile Transform(this RepositoryContent content, Octokit.Repository repository, GitHubClient client)
        {
            return new File(content, repository, client);
        }
    }
}