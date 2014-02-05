using Nest.Resolvers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class CompletionMapping : IElasticType
	{
		[JsonProperty("name")]
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "completion" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("payloads")]
		public bool? Payloads { get; set; }

		[JsonProperty("preserve_separators")]
		public bool? PreserveSeparators { get; set; }

		[JsonProperty("preserve_position_increments")]
		public bool? PreservePositionIncrements { get; set; }

		[JsonProperty("max_input_len")]
		public int? MaxInputLength { get; set; }
	}
}