using System;
using System.Collections.Generic;

namespace Sourcelyzer.Analyzing
{
    public class AnalyzingConfiguration
    {
        public IList<IAnalyzer> Analyzers { get; } = new List<IAnalyzer>();

        public void AddAnalyzer(IAnalyzer analyzer)
        {
            if (analyzer == null) throw new ArgumentNullException(nameof(analyzer));

            Analyzers.Add(analyzer);
        }
    }
}
