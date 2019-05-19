using System;

namespace Sourcelyzer.GitHub
{
    internal interface ICollectorOptions : IGitHubOptions
    {
        Collecting.Filter.Options Filter { get; }

        void SetGitHubUri(Uri uri);
    }
}
