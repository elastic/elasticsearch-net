using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class RangeBucket : BucketBase, IBucket
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("from")]
		public double? From { get; set; }
		
		[JsonProperty("from_as_string")]
		public string FromAsString { get; set; }

		[JsonProperty("to")]
		public double? To { get; set; }

		[JsonProperty("to_as_string")]
		public string ToAsString { get; set; }

		[JsonProperty("doc_count")]
		public long DocCount { get; set; }

		internal override bool Matches(JToken source) => source.Value<string>() == Key;
	}
}