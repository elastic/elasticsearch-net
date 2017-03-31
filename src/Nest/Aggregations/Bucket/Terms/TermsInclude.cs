using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TermsIncludeJsonConverter))]
	public class TermsInclude
	{
		[JsonIgnore]
		public string Pattern { get; set; }

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

		public TermsInclude(string pattern)
		{
			Pattern = pattern;
		}

		public TermsInclude(IEnumerable<string> values)
		{
			Values = values;
		}

		public TermsInclude(long partition, long numberOfPartitions)
		{
			Partition = partition;
			NumberOfPartitions = numberOfPartitions;
		}
	}
}
