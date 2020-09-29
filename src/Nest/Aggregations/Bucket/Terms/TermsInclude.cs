// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Filters which terms to include in the response
	/// </summary>
	[JsonFormatter(typeof(TermsIncludeFormatter))]
	public class TermsInclude
	{
		/// <summary>
		/// Creates an instance of <see cref="TermsInclude" /> that uses a regular expression pattern
		/// to determine the terms to include in the response
		/// </summary>
		/// <param name="pattern">The regular expression pattern</param>
		public TermsInclude(string pattern) => Pattern = pattern;

		/// <summary>
		/// Creates an instance of <see cref="TermsInclude" /> that uses a collection of terms
		/// to include in the response
		/// </summary>
		/// <param name="values">The exact terms to include</param>
		public TermsInclude(IEnumerable<string> values) => Values = values;

		/// <summary>
		/// Creates an instance of <see cref="TermsInclude" /> that partitions the terms into a number of
		/// partitions to receive in multiple requests. Used to process many unique terms
		/// </summary>
		/// <param name="partition">The 0-based partition number for this request</param>
		/// <param name="numberOfPartitions">The total number of partitions</param>
		public TermsInclude(long partition, long numberOfPartitions)
		{
			Partition = partition;
			NumberOfPartitions = numberOfPartitions;
		}

		/// <summary>
		/// The total number of paritions we are interested in
		/// </summary>
		[DataMember(Name ="num_partitions")]
		public long? NumberOfPartitions { get; set; }

		/// <summary>
		/// The current partition of terms we are interested in
		/// </summary>
		[DataMember(Name ="partition")]
		public long? Partition { get; set; }

		/// <summary>
		/// The regular expression pattern to determine terms to include in the response
		/// </summary>
		[IgnoreDataMember]
		public string Pattern { get; set; }

		/// <summary>
		/// Collection of terms to include in the response
		/// </summary>
		[IgnoreDataMember]
		public IEnumerable<string> Values { get; set; }
	}
}
