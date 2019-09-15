namespace Sourcelyzer.Reporting
{
    public class ReportingBuilder
    {
        public ReportingBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        }

        public SourcelyzerBuilder Builder { get; }

        public ReportingConfiguration Configuration { get; } = new ReportingConfiguration();
    }
}
