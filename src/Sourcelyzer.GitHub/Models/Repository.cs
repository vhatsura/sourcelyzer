using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq.Experimental;
using Sourcelyzer.GitHub.Extensions;
using Sourcelyzer.Model;

namespace Sourcelyzer.GitHub.Models
{
    internal class Repository : IRepository
    {
        private readonly Octokit.Repository _repository;
        private readonly Octokit.GitHubClient _client;

        private IEnumerable<IFile> _files;
        
        internal Repository(Octokit.Repository repository, Octokit.GitHubClient client)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public long Id => _repository.Id;
        public string Owner => _repository.Owner.Login;
        public string Name => _repository.Name;

        public async Task<IEnumerable<IFile>> GetFilesAsync()
        {
            if (_files == null)
            {
                var contents = await _client.Repository.Content.GetAllContents(_repository.Owner.Login, _repository.Name);

                _files = contents
                    .Await((content, token) => GetContents(content))
                    .SelectMany(c => c)
                    .Select(content => content.Transform(_repository, _client));    
            }

            return _files;
        }

        private async Task<IEnumerable<Octokit.RepositoryContent>> GetContents(Octokit.RepositoryContent content)
        {
            switch (content.Type.Value)
            {
                case Octokit.ContentType.File:
                    return content.Yield();
                case Octokit.ContentType.Dir:
                    var dirContent = await _client.Repository.Content.GetAllContents(_repository.Owner.Login,
                        _repository.Name, content.Path);
                    return dirContent.Await((c, token) => GetContents(c)).SelectMany(c => c);
                default:
                    return Enumerable.Empty<Octokit.RepositoryContent>();
            }
        }
    }
}