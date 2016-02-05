using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class KeyedBucket : BucketBase
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("key_as_string")]
		public string KeyAsString { get; set; }

		[JsonProperty("doc_count")]
		public long? DocCount { get; set; }

		internal override bool Matches(JToken source) => source.Value<string>() == Key;
	}
}
