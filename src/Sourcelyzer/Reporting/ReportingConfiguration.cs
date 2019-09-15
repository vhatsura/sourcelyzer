using System;
using System.Collections.Generic;

namespace Sourcelyzer.Reporting
{
    public class ReportingConfiguration
    {
        public IList<IReporter> Reporters { get; } = new List<IReporter>();

        public void AddReporter(IReporter reporter)
        {
            if (reporter == null) throw new ArgumentNullException(nameof(reporter));

            Reporters.Add(reporter);
        }
    }
}
