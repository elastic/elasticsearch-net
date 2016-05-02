using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class RangeBucket : BucketBase, IBucket
	{
		[JsonProperty("key")]
		public string Key { get; internal set; }

		[JsonProperty("from")]
		public double? From { get; internal set; }

		[JsonProperty("from_as_string")]
		public string FromAsString { get; internal set; }

		[JsonProperty("to")]
		public double? To { get; internal set; }

		[JsonProperty("to_as_string")]
		public string ToAsString { get; internal set; }

		[JsonProperty("doc_count")]
		public long DocCount { get; internal set; }

		internal override bool Matches(JToken source) => source.Value<string>() == this.Key;
	}
}
