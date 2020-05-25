// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// A bucket for an <see cref="IpRangeAggregation"/>
	/// </summary>
	public class IpRangeBucket : BucketBase
	{
		public IpRangeBucket(IReadOnlyDictionary<string, IAggregate> dict) : base(dict) { }

		/// <summary>
		/// The count of documents in the bucket
		/// </summary>
		public long DocCount { get; set; }

		/// <summary>
		/// The IP address from
		/// </summary>
		public string From { get; set; }

		/// <summary>
		/// The key for the bucket
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// The IP address to
		/// </summary>
		public string To { get; set; }
	}
}
