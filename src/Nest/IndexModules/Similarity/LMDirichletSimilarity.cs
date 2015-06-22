using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class LMDirichletSimilarity : SimilarityBase
	{
		public LMDirichletSimilarity()
		{
			this.Type = "LMDirichlet";
		}

		[JsonProperty("mu")]
		public int Mu { get; set; }
	}
}
