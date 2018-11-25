namespace Sourcelyzer.Reporters
{
    public class ReporterBuilder
    {
        public SourcelyzerBuilder Builder { get; }

        public ReportersOptions Options { get; } = new ReportersOptions();
        
        public ReporterBuilder(SourcelyzerBuilder builder)
        {
            Builder = builder;
        } 
    }
}