using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class IBSimilarity : SimilarityBase
	{
		public override string Type => "IB";

		[JsonProperty("distribution")]
		public string Distribution { get; set; }

		[JsonProperty("lambda")]
		public string Lambda { get; set; }
	}
}
