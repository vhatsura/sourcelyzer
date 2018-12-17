using Sourcelyzer.Analyzing;
using Sourcelyzer.Collecting;
using Sourcelyzer.Reporting;

namespace Sourcelyzer
{
    public class SourcelyzerBuilder
    {
        public CollectorBuilder Collecting { get; }

        public AnalyzerBuilder Analyzing { get; }

        public ReporterBuilder Reporting { get; }

        public SourcelyzerBuilder()
        {
            Collecting = new CollectorBuilder(this);
            Analyzing = new AnalyzerBuilder(this);
            Reporting = new ReporterBuilder(this);
        }

        public Sourcelyzer Build()
        {
            return new Sourcelyzer(Collecting.Options.Collectors, Analyzing.Options.Analyzers,
                Reporting.Options.Reporters);
        }
    }
}