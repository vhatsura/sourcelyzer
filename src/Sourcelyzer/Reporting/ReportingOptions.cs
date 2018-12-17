using System;
using System.Collections.Generic;

namespace Sourcelyzer.Reporting
{
    public class ReportingOptions
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