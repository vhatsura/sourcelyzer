using Sourcelyzer.Reporting;

namespace Sourcelyzer.GitHub.Reporting
{
    public static class BuilderExtensions
    {
        public static SourcelyzerBuilder AsGithubIssue(this ReporterBuilder reportingBuilder)
        {
            return reportingBuilder.Builder;
        }
    }
}