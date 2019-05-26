using Sourcelyzer.Reporting;

namespace Sourcelyzer.GitHub.Reporting
{
    public static class ReportingBuilderExtensions
    {
        public static SourcelyzerBuilder AsGitHubIssue(this ReportingBuilder reportingBuilder)
        {
            var reporter = new GitHubIssueReporter(Options.Registered);

            reportingBuilder.Configuration.AddReporter(reporter);

            return reportingBuilder.Builder;
        }
    }
}
