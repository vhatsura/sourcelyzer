using System;
using System.Collections.Generic;

namespace Sourcelyzer.Analyzers
{
    public class AnalyzersOptions
    {
        private readonly IList<IAnalyzer> _analyzers = new List<IAnalyzer>();

        public IList<IAnalyzer> Analyzers => _analyzers;

        public void AddAnalyzer(IAnalyzer analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));
            
            _analyzers.Add(analyzer);
        }    
    }
}