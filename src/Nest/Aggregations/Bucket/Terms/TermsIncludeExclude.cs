using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(TermsIncludeExcludeJsonConverter))]
	public class TermsIncludeExclude
	{
		[JsonProperty("pattern")]
		public string Pattern { get; set; }

		[JsonProperty("flags")]
		public string Flags { get; set; }

		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }

		//TODO Better types for this in 6.0
		/// <summary>
		/// Only valid on terms include, the current partition of terms we are interested in
		/// </summary>
		[JsonProperty("partition")]
		public long? Partition { get; set; }

		/// <summary>
		/// Only valid on terms include, the total number of paritions we are interested in
		/// </summary>
		[JsonProperty("num_partitions")]
		public long? NumberOfPartitions { get; set; }
	}
}
