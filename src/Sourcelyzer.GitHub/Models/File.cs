using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Sourcelyzer.Model;

namespace Sourcelyzer.GitHub.Models
{
    [DebuggerDisplay("{" + nameof(Path) + "}")]
    internal class File : IFile
    {
        private readonly RepositoryContent _content;

        private readonly AsyncLazy<string> _stringContentLazy;

        public File(RepositoryContent content, Octokit.Repository repository, GitHubClient client)
        {
            _content = content;
            var localRepository = repository;
            _stringContentLazy = new AsyncLazy<string>(async () =>
            {
                var fileContent = await client.Repository.Content.GetAllContents(
                    localRepository.Owner.Login,
                    localRepository.Name,
                    _content.Path);

                return fileContent.First().Content;
            });
        }

        public string Path => _content.Path;

        public Task<string> GetContentAsync()
        {
            return _stringContentLazy.Value;
        }
    }
}
