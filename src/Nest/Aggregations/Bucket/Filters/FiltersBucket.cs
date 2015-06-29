
using System.Collections.Generic;

namespace Nest
{
    public class FiltersBucket : IAggregation
    {
        public FiltersBucket(IEnumerable<IAggregation> items)
        {
            Items = items;
        }

        public FiltersBucket(AggregationsHelper helper)
        {
            Aggregations = helper;
        }
        public AggregationsHelper Aggregations { get; private set; }

        public IEnumerable<IAggregation> Items { get; private set; }
    }
}
