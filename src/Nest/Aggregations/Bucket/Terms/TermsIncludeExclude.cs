using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TermsIncludeExcludeJsonConverter))]
	public class TermsIncludeExclude
	{
		[JsonIgnore]
		[Obsolete("Deprecated in 2.0. Flags can no longer be specified")]
		public string Flags { get; set; }

		/// <summary>
		/// The total number of paritions we are interested in
		/// <para>IMPORTANT: Only valid on terms include</para>
		/// </summary>
		[JsonProperty("num_partitions")]
		public long? NumberOfPartitions { get; set; }

		//TODO Better types for this in 6.0
		/// <summary>
		/// The current partition of terms we are interested in
		/// <para>IMPORTANT: Only valid on terms include</para>
		/// </summary>
		[JsonProperty("partition")]
		public long? Partition { get; set; }

		[JsonIgnore]
		public string Pattern { get; set; }

		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }
	}
}
