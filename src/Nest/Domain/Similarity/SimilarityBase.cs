using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class SimilarityBase
	{
		[JsonProperty("type")]
		public string Type { get; protected set; }

		[JsonProperty("normalization")]
		public string Normalization { get; set; }

		[JsonProperty("normalization.h1.c")]
		public string NormalizationH1C { get; set; }

		[JsonProperty("normalization.h2.c")]
		public string NormalizationH2C { get; set; }

		[JsonProperty("normalization.h3.c")]
		public string NormalizationH3C { get; set; }

		[JsonProperty("normalization.z.z")]
		public string NormalizationZZ { get; set; }
	}
}
