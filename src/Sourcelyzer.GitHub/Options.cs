using System;
using Octokit;

namespace Sourcelyzer.GitHub
{
    internal class Options : ICollectorOptions
    {
        // todo: add synchronization
        private static Options _registeredOptions;

        private Options()
        {
        }

        internal static Options Registered => _registeredOptions ??
                                              throw new InvalidOperationException(
                                                  "GitHub collector was not initialized yet.");

        internal ICredentialStore CredentialStore { private get; set; }

        private Uri GitHubUri { get; set; } = new Uri("https://github.com");

        internal string ProductName { private get; set; }

        public GitHubClient Client => new GitHubClient(new ProductHeaderValue(ProductName), CredentialStore, GitHubUri);

        public Collecting.Filter.Options Filter { get; } = new Collecting.Filter.Options();

        public void SetGitHubUri(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));

            if (!uri.IsAbsoluteUri) throw new ArgumentException("The uri must be absolute.", nameof(uri));

            if (uri.IsFile) throw new ArgumentException("The uri must not be file.", nameof(uri));

            GitHubUri = uri;
        }

        internal static Options Create()
        {
            _registeredOptions = new Options();
            return _registeredOptions;
        }
    }
}
