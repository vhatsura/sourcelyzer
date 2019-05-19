namespace Sourcelyzer.Reporting
{
    public class ReportingBuilder
    {
        public SourcelyzerBuilder Builder { get; }

        public ReportingConfiguration Configuration { get; } = new ReportingConfiguration();

        public ReportingBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }
    }
}