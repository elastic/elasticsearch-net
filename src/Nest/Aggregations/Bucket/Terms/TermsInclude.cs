using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Filters which terms to include in the response
	/// </summary>
	[JsonConverter(typeof(TermsIncludeJsonConverter))]
	public class TermsInclude
	{
		/// <summary>
		/// The regular expression pattern to determine terms to include in the response
		/// </summary>
		[JsonIgnore]
		public string Pattern { get; set; }

		/// <summary>
		/// Collection of terms to include in the response
		/// </summary>
		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }

		/// <summary>
		/// The current partition of terms we are interested in
		/// </summary>
		[JsonProperty("partition")]
		public long? Partition { get; set; }

		/// <summary>
		/// The total number of paritions we are interested in
		/// </summary>
		[JsonProperty("num_partitions")]
		public long? NumberOfPartitions { get; set; }

		/// <summary>
		/// Creates an instance of <see cref="TermsInclude"/> that uses a regular expression pattern
		/// to determine the terms to include in the response
		/// </summary>
		/// <param name="pattern">The regular expression pattern</param>
		public TermsInclude(string pattern) => Pattern = pattern;

		/// <summary>
		/// Creates an instance of <see cref="TermsInclude"/> that uses a collection of terms
		/// to include in the response
		/// </summary>
		/// <param name="values">The exact terms to include</param>
		public TermsInclude(IEnumerable<string> values) => Values = values;

		/// <summary>
		/// Creates an instance of <see cref="TermsInclude"/> that partitions the terms into a number of
		/// partitions to receive in multiple requests. Used to process many unique terms
		/// </summary>
		/// <param name="partition">The 0-based partition number for this request</param>
		/// <param name="numberOfPartitions">The total number of partitions</param>
		public TermsInclude(long partition, long numberOfPartitions)
		{
			Partition = partition;
			NumberOfPartitions = numberOfPartitions;
		}
	}
}
