using Sourcelyzer.Analyzing;
using Sourcelyzer.Collecting;
using Sourcelyzer.Reporting;

namespace Sourcelyzer
{
    public class SourcelyzerBuilder
    {
        public CollectingBuilder Collecting { get; }

        public AnalyzingBuilder Analyzing { get; }

        public ReportingBuilder Reporting { get; }

        public SourcelyzerBuilder()
        {
            Collecting = new CollectingBuilder(this);
            Analyzing = new AnalyzingBuilder(this);
            Reporting = new ReportingBuilder(this);
        }

        public Sourcelyzer Build()
        {
            return new Sourcelyzer(Collecting.Configuration.Collectors, Analyzing.Configuration.Analyzers,
                Reporting.Configuration.Reporters);
        }
    }
}