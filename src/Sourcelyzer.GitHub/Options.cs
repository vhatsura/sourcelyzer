using System;
using Octokit;

namespace Sourcelyzer.GitHub
{
    internal class Options : ICollectorOptions
    {
        private Options()
        {
        }

        // todo: add synchronization
        private static Options _registeredOptions;

        internal static Options Registered => _registeredOptions ??
                                              throw new InvalidOperationException(
                                                  "GitHub collector was not initialized yet.");

        internal static Options Create()
        {
            _registeredOptions = new Options();
            return _registeredOptions;
        }

        internal ICredentialStore CredentialStore { private get; set; }

        private Uri GitHubUri { get; set; } = new Uri("https://github.com");

        public GitHubClient Client => new GitHubClient(new ProductHeaderValue(ProductName), CredentialStore, GitHubUri);

        internal string ProductName { private get; set; }

        public Collecting.Filter.Options Filter { get; } = new Collecting.Filter.Options();

        public void SetGitHubUri(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            if (!uri.IsAbsoluteUri) throw new ArgumentException("The uri must be absolute.", nameof(uri));
            if (uri.IsFile) throw new ArgumentException("The uri must not be file.", nameof(uri));

            GitHubUri = uri;
        }
    }
}
