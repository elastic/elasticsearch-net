using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest
{
    public class TermsAggregate : MultiBucketAggregate<KeyedBucket>
    {
		[JsonProperty("doc_count_error_upper_bound")]
		public long DocCountErrorUpperBound { get; set; }

		[JsonProperty("sum_other_doc_count")]
		public long SumOtherDocCount { get; set; }
    }
}
