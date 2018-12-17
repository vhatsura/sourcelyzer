using System;
using System.Collections.Generic;

namespace Sourcelyzer.Collecting
{
    public class CollectingOptions
    {
        private readonly IList<ICollector> _collectors = new List<ICollector>();

        public IList<ICollector> Collectors => _collectors;

        public void AddCollector(ICollector collector)
        {
            if (collector == null)
                throw new ArgumentNullException(nameof(collector));

            _collectors.Add(collector);
        }
    }
}