using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

namespace Nest
{
	[JsonObject]
	public class SuggestOption<T> where T : class
	{
		[JsonProperty("text")]
		public string Text { get; internal set; }

		[JsonProperty("_score")]
		public double Score { get; internal set; }

		[JsonProperty("_index")]
		public IndexName Index { get; internal set; }

		[JsonProperty("_type")]
		public TypeName Type { get; internal set; }

		[JsonProperty("_id")]
		public Id Id { get; internal set; }

		[JsonProperty("_source")]
		public T Source { get; internal set; }

		[JsonProperty("contexts")]
		public IDictionary<string, IEnumerable<Context>> Contexts { get; internal set; }

		[JsonProperty("highlighted")]
		public string Highlighted { get; internal set; }

		[JsonProperty("collate_match")]
		public bool CollateMatch { get; internal set; }

	}
}
