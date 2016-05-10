using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public class SignificantTermsBucket : BucketBase, IBucket
	{
		[JsonProperty("key")]
		public string Key { get; internal set; }

		[JsonProperty("bg_count")]
		public long BgCount { get; internal set; }

		[JsonProperty("doc_count")]
		public long DocCount { get; internal set; }

		[JsonProperty("score")]
		public double Score { get; internal set; }

		internal override bool Matches(JToken source) => source.Value<string>() == this.Key;
	}
}
