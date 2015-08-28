using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class DFRSimilarity : SimilarityBase
	{
		public override string Type => "DFR";

		[JsonProperty("basic_model")]
		public string BasicModel { get; set; }

		[JsonProperty("after_effect")]
		public string AfterEffect { get; set; }
	}
}