using System;
using System.Linq;
using System.Threading;
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
            const string auditLabelName = "audit";
            
            var labels =
                await _options.Client.Issue.Labels.GetAllForRepository(result.Repository.Owner, result.Repository.Name);

            if (!labels.Any(x => x.Name.Equals(auditLabelName, StringComparison.OrdinalIgnoreCase)))
            {
                await _options.Client.Issue.Labels.Create(result.Repository.Owner, result.Repository.Name,
                    new NewLabel(auditLabelName, "0905c6"));              
            }

            var issues = await _options.Client.Issue.GetAllForRepository(result.Repository.Owner,
                result.Repository.Name,
                new RepositoryIssueRequest
                {
                    Labels = {auditLabelName, result.GetType().Name.ToLowerInvariant()}
                });

            if (!issues.Any())
            {
                await _options.Client.Issue.Create(
                    result.Repository.Owner,
                    result.Repository.Name,
                    new NewIssue(result.Title)
                    {
                        Body = result.ToMarkdown(),
                        Labels = {auditLabelName, result.GetType().Name.ToLowerInvariant()}
                    });    
            }
        }
    }
}
