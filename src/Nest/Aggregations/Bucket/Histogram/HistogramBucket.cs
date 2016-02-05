using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class HistogramBucket : BucketBase, IBucket
	{
		[JsonProperty("key")]
		public long Key { get; set; }

		[JsonProperty("key_as_string")]
		public string KeyAsString { get; set; }
		
		[JsonProperty("doc_count")]
		public long DocCount { get; set; }

		internal override bool Matches(JToken source) => source.Value<long>() == Key;
	}
}