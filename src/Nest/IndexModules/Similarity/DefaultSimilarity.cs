using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class DefaultSimilarity : SimilarityBase
	{
		public override string Type => "default";

		[JsonProperty("discount_overlaps")]
		public bool DiscountOverlaps { get; set; }
	}
}
