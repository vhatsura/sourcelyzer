using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq.Experimental;
using Octokit;
using Sourcelyzer.Collecting;
using Sourcelyzer.GitHub.Extensions;
using Sourcelyzer.Model;

namespace Sourcelyzer.GitHub.Collecting
{
    internal class GitHubCollector : ICollector
    {
        private readonly ICollectorOptions _options;

        internal GitHubCollector(ICollectorOptions options)
        {
            _options = options;
        }

        public async Task<IEnumerable<IRepository>> GetRepositoriesAsync()
        {
            var client = _options.Client;

            var repositories = _options.Filter.OrganizationsToAnalyze.Any()
                ? GetRepositoriesFromOrganizations(client, _options.Filter.OrganizationsToAnalyze)
                : await client.Repository.GetAllPublic();

            return repositories
                .Where(repository => _options.Filter.RepositoryFilters.All(f => f(repository)))
                .Select(repository => repository.Transform(client));
        }

        private IEnumerable<Repository> GetRepositoriesFromOrganizations(GitHubClient client,
            IEnumerable<string> organizations)
        {
            return organizations
                .Await((org, token) => client.Repository.GetAllForOrg(org))
                .SelectMany(repo => repo);
        }
    }
}
