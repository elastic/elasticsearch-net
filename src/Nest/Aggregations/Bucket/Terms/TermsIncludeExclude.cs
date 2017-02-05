using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
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
