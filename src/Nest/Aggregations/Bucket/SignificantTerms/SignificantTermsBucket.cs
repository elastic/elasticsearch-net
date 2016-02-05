using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class SignificantTermsBucket : BucketBase, IBucket
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("bg_count")]
		public long BgCount { get; set; }

		[JsonProperty("doc_count")]
		public long DocCount { get; set; }

		[JsonProperty("score")]
		public double Score { get; set; }

		internal override bool Matches(JToken source) => source.Value<string>() == Key;
	}
}