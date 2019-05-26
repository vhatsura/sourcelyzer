using Octokit;
using Sourcelyzer.GitHub.Models;
using Sourcelyzer.Model;
using Repository = Octokit.Repository;

namespace Sourcelyzer.GitHub.Extensions
{
    public static class RepositoryExtensions
    {
        public static IRepository Transform(this Repository repository, GitHubClient client)
        {
            return new Models.Repository(repository, client);
        }

        public static IFile Transform(this RepositoryContent content, Repository repository, GitHubClient client)
        {
            return new File(content, repository, client);
        }
    }
}
