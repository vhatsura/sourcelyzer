using System.Linq;
using System.Threading.Tasks;
using Sourcelyzer.Model;

namespace Sourcelyzer.GitHub.Models
{
    internal class File : IFile
    {
        private readonly Octokit.RepositoryContent _content;
        private readonly Octokit.Repository _repository;
        private readonly Octokit.GitHubClient _client;

        public File(Octokit.RepositoryContent content, Octokit.Repository repository, Octokit.GitHubClient client)
        {
            _content = content;
            _repository = repository;
            _client = client;
        }

        public string Path => _content.Path;

        public async Task<string> GetContentAsync()
        {
            if (_stringContent == null)
            {
                var packagesFile =
                    await _client.Repository.Content.GetAllContents(_repository.Owner.Login, _repository.Name,
                        _content.Path);
                _stringContent = packagesFile.First().Content;
            }

            return _stringContent;
        }

        private string _stringContent;
    }
}