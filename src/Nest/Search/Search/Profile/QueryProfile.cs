using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
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
		/// The time that this query took, inclusive of all children
		/// </summary>
		[JsonProperty("time")]
		public Time Time { get; internal set; }

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
