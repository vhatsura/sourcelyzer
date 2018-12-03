 using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;
 using Sourcelyzer.Analyzing;
 using Sourcelyzer.Collecting;
 using Sourcelyzer.Reporting;

 namespace Sourcelyzer
{
    public class Sourcelyzer
    {
        private IReadOnlyCollection<ICollector> Collectors { get; }
        private IReadOnlyCollection<IAnalyzer> Analyzers { get; }
        private IReadOnlyCollection<IReporter> Reporters { get; }

        public Sourcelyzer(IEnumerable<ICollector> collectors, IEnumerable<IAnalyzer> analyzers,
            IEnumerable<IReporter> reporters)
        {
            Collectors = collectors?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(collectors));
            Analyzers = analyzers?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(analyzers));
            Reporters = reporters?.ToList().AsReadOnly() ?? throw new ArgumentNullException(nameof(reporters));
        }

        public async Task RunAsync()
        {
            foreach (var collector in Collectors)
            {
                var repositories = await collector.GetRepositoriesAsync();

                foreach (var repository in repositories)
                {
                    foreach (var analyzer in Analyzers)
                    {
                        var results = await analyzer.AnalyzeAsync(repository);

                        foreach (var reporter in Reporters)
                        {
                            reporter.Report(results);
                        }
                    }
                }
            }
        }
    }
}