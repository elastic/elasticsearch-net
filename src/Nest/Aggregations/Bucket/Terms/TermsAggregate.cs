using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
    public class TermsAggregate : MultiBucketAggregate<KeyedBucketItem>
    {
		public long? DocCountErrorUpperBound { get; set; }
		public long? SumOtherDocCount { get; set; }
    }
}
