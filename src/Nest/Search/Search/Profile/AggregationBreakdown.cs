using System.Runtime.Serialization;
using System.Runtime.Serialization;

namespace Nest
{
	public class AggregationBreakdown
	{
		[DataMember(Name ="build_aggregation")]
		public long BuildAggregation { get; internal set; }

		[DataMember(Name ="build_aggregation_count")]
		public long BuildAggregationCount { get; internal set; }

		[DataMember(Name ="collect")]
		public long Collect { get; internal set; }

		[DataMember(Name ="collect_count")]
		public long CollectCount { get; internal set; }

		[DataMember(Name ="initialize")]
		public long Initialize { get; internal set; }

		[DataMember(Name ="intialize_count")]
		public long InitializeCount { get; internal set; }

		[DataMember(Name ="reduce")]
		public long Reduce { get; internal set; }

		[DataMember(Name ="reduce_count")]
		public long ReduceCount { get; internal set; }
	}
}
