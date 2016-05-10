using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class HistogramBucket : BucketBase, IBucket
	{
		[JsonProperty("key")]
		public long Key { get; internal set; }

		[JsonProperty("key_as_string")]
		public string KeyAsString { get; internal set; }

		[JsonProperty("doc_count")]
		public long DocCount { get; internal set; }

		internal override bool Matches(JToken source) => source.Value<long>() == this.Key;
	}
}
