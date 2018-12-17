namespace Sourcelyzer.Reporting
{
    public class ReporterBuilder
    {
        public SourcelyzerBuilder Builder { get; }

        public ReportingOptions Options { get; } = new ReportingOptions();

        public ReporterBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }
    }
}