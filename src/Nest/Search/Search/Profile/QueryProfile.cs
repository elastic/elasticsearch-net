using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class QueryProfile
	{
		/// <summary>
		/// The lucene class name for the type of query
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; internal set; }

		/// <summary>
		/// The lucene explanation text for the query
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; internal set; }

		/// <summary>
		/// The time that this query took in nanoseconds
		/// </summary>
		[JsonProperty("time_in_nanos")]
		public long TimeInNanoseconds { get; internal set; }

		/// <summary>
		/// Detailed stats about how the time was spent
		/// </summary>
		[JsonProperty("breakdown")]
		public QueryBreakdown Breakdown { get; internal set; }

		/// <summary>
		/// Sub-queries of this query
		/// </summary>
		[JsonProperty("children")]
		public IEnumerable<QueryProfile> Children { get; internal set; }
	}
}
