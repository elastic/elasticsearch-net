using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class AggregationBreakdown
	{
		[JsonProperty("reduce")]
		public long Reduce { get; internal set; }

		[JsonProperty("build_aggregation")]
		public long BuildAggregation { get; internal set; }

		[JsonProperty("build_aggregation_count")]
		public long BuildAggregationCount { get; internal set; }

		[JsonProperty("initialize")]
		public long Initialize { get; internal set; }

		[JsonProperty("intialize_count")]
		public long InitializeCount { get; internal set; }

		[JsonProperty("reduce_count")]
		public long ReduceCount { get; internal set; }

		[JsonProperty("collect")]
		public long Collect { get; internal set; }

		[JsonProperty("collect_count")]
		public long CollectCount { get; internal set; }
	}
}
