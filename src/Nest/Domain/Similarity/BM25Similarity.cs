using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class BM25Similarity : SimilarityBase
	{
		public BM25Similarity()
		{
			this.Type = "BM25";
		}

		[JsonProperty("k1")]
		public double K1 { get; set; }

		[JsonProperty("b")]
		public double B { get; set; }

		[JsonProperty("discount_overlaps")]
		public bool DiscountOverlaps { get; set; }
	}
}
