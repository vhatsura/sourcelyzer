using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        private const string AuditLabelName = "audit";

        public async Task ReportAsync(IAnalyzerResult result)
        {
            await EnsureRepositoryIsConfiguredAsync(result.Repository.Owner, result.Repository.Name);

            var issues = await _options.Client.Issue.GetAllForRepository(result.Repository.Owner,
                result.Repository.Name,
                new RepositoryIssueRequest
                {
                    Labels = {AuditLabelName, result.GetType().Name.ToLowerInvariant()},
                    State = ItemStateFilter.Open
                });

            if (issues.Any())
            {
                foreach (var (issue, technicalInfo) in ExtractTechnicalInfo(issues))
                {
                    var status = result.ActualizeStatus(technicalInfo);
                    switch (status)
                    {
                        case Status.Another:
                            continue;
                        case Status.UpToDate:
                            return;
                        case Status.NeedToUpdate:
                        {
                            await _options.Client.Issue.Update(result.Repository.Owner, result.Repository.Name,
                                issue.Id,
                                new IssueUpdate {Title = result.Title, Body = CreateBodyForIssue(result)});
                            return;
                        }
                    }
                }
            }

            await CreateIssueAsync(result);
        }

        private async Task EnsureRepositoryIsConfiguredAsync(string owner, string repositoryName)
        {
            var labels = await _options.Client.Issue.Labels.GetAllForRepository(owner, repositoryName);

            if (!labels.Any(x => x.Name.Equals(AuditLabelName, StringComparison.OrdinalIgnoreCase)))
            {
                await _options.Client.Issue.Labels.Create(owner, repositoryName,
                    new NewLabel(AuditLabelName, "0905c6"));
            }

            //todo: check if issue is disabled. If issues is disabled try to enable it
        }

        private string CreateBodyForIssue(IAnalyzerResult result)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(result.ToMarkdown());
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine("<details><summary>Technical details</summary>");

            stringBuilder.AppendLine(
                $"    <p>{string.Join(";", result.TechnicalInfo.Select(x => $"{x.Key}:{x.Value}"))}</p>");

            stringBuilder.AppendLine("</details>");

            return stringBuilder.ToString();
        }

        private async Task CreateIssueAsync(IAnalyzerResult result)
        {
            var issue = new NewIssue(result.Title)
            {
                Body = CreateBodyForIssue(result),
                Labels = {AuditLabelName, result.GetType().Name.ToLowerInvariant()}
            };

            await _options.Client.Issue.Create(
                result.Repository.Owner,
                result.Repository.Name,
                issue);
        }

        private IEnumerable<(Issue, IDictionary<string, string>)> ExtractTechnicalInfo(IReadOnlyList<Issue> issues)
        {
            foreach (var issue in issues)
            {
                var lastIndexOf = issue.Body.LastIndexOf($"<details><summary>Technical details</summary>",
                    StringComparison.InvariantCultureIgnoreCase);

                if (lastIndexOf < 0) continue;

                var details = issue.Body.Substring(lastIndexOf);

                var techDetails = XDocument.Parse(details).Descendants("p").Single().Value;

                yield return (issue, !string.IsNullOrWhiteSpace(techDetails)
                    ? techDetails.Split(';').Select(x =>
                        {
                            var index = x.IndexOf(':');
                            var key = x.Substring(0, index);
                            var value = x.Substring(index + 1);
                            return (Key: key, Value: value);
                        })
                        .ToDictionary(x => x.Key, x => x.Value)
                    : new Dictionary<string, string>());
            }
        }
    }
}
