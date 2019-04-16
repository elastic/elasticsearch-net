using System.Runtime.Serialization;

namespace Nest
{
	public class AggregationProfile
	{
		/// <summary>
		/// Detailed stats about how the time was spent
		/// </summary>
		[DataMember(Name ="breakdown")]
		public AggregationBreakdown Breakdown { get; internal set; }

		/// <summary>
		/// The user defined name of the aggregation
		/// </summary>
		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		/// <summary>
		/// The time this aggregation took, in nanoseconds
		/// </summary>
		[DataMember(Name ="time_in_nanos")]
		public long TimeInNanoseconds { get; internal set; }

		/// <summary>
		/// The Elasticsearch aggregation type
		/// </summary>
		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}
}
