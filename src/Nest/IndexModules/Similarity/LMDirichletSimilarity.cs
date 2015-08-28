using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class LMDirichletSimilarity : SimilarityBase
	{
		public override string Type => "LMDirichlet";

		[JsonProperty("mu")]
		public int Mu { get; set; }
	}
}
