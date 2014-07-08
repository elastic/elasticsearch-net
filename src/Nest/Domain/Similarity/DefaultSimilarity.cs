using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class DefaultSimilarity : SimilarityBase
	{
		public DefaultSimilarity()
		{
			this.Type = "default";
		}

		[JsonProperty("discount_overlaps")]
		public bool DiscountOverlaps { get; set; }
	}
}
