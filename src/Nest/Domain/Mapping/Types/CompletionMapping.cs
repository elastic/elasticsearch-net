using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class CompletionMapping : IElasticType
	{
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "completion" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("analyzer")]
		public string Analyzer { get; set; }

		[JsonProperty("payloads")]
		public bool? Payloads { get; set; }

		[JsonProperty("preserve_separators")]
		public bool? PreserveSeparators { get; set; }

		[JsonProperty("preserve_position_increments")]
		public bool? PreservePositionIncrements { get; set; }

		[JsonProperty("max_input_len")]
		public int? MaxInputLength { get; set; }

		[JsonProperty("context")]
		public IDictionary<string, ISuggestContext> Context { get ;set;}
	}
}