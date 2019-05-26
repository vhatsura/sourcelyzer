using System;
using System.Collections.Generic;

namespace Sourcelyzer.Collecting
{
    public class CollectingConfiguration
    {
        public IList<ICollector> Collectors { get; } = new List<ICollector>();

        public void AddCollector(ICollector collector)
        {
            if (collector == null) throw new ArgumentNullException(nameof(collector));

            Collectors.Add(collector);
        }
    }
}
