using System;
using System.Collections.Generic;

namespace Sourcelyzer.Reporters
{
    public class ReportersOptions
    {
        private readonly IList<IReporter> _reporters = new List<IReporter>();

        public IList<IReporter> Reporters => _reporters;

        public void AddCollector(IReporter reporter)
        {
            if (reporter == null)
                throw new ArgumentNullException(nameof(reporter));
            
            _reporters.Add(reporter);
        }        
    }
}