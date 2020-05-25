// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class QueryProfile
	{
		/// <summary>
		/// Detailed stats about how the time was spent
		/// </summary>
		[DataMember(Name ="breakdown")]
		public QueryBreakdown Breakdown { get; internal set; }

		/// <summary>
		/// Sub-queries of this query
		/// </summary>
		[DataMember(Name ="children")]
		public IEnumerable<QueryProfile> Children { get; internal set; }

		/// <summary>
		/// The lucene explanation text for the query
		/// </summary>
		[DataMember(Name ="description")]
		public string Description { get; internal set; }

		/// <summary>
		/// The time that this query took in nanoseconds
		/// </summary>
		[DataMember(Name ="time_in_nanos")]
		public long TimeInNanoseconds { get; internal set; }

		/// <summary>
		/// The lucene class name for the type of query
		/// </summary>
		[DataMember(Name ="type")]
		public string Type { get; internal set; }
	}
}
