using System;
using System.Threading.Tasks;
using Octokit;
using Sourcelyzer.Model.Analyzing;
using Sourcelyzer.Reporting;

namespace Sourcelyzer.GitHub.Reporting
{
    internal class GitHubIssueReporter : IReporter
    {
        private readonly IGitHubOptions _options;

        internal GitHubIssueReporter(IGitHubOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task ReportAsync(IAnalyzerResult result)
        {
            await _options.Client.Issue.Create(
                result.Repository.Owner,
                result.Repository.Name,
                new NewIssue(result.Title)
                {
                    Body = result.ToMarkdown()
                });
        }
    }
}
