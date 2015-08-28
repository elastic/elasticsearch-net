using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class LMJelinekSimilarity : SimilarityBase
	{
		public override string Type => "LMJelinekMercer";

		[JsonProperty("lambda")]
		public double Lambda { get; set; }
	}
}
