using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface ISimilarity
	{
		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("normalization")]
		string Normalization { get; set; }

		[JsonProperty("normalization.h1.c")]
		string NormalizationH1C { get; set; }

		[JsonProperty("normalization.h2.c")]
		string NormalizationH2C { get; set; }

		[JsonProperty("normalization.h3.c")]
		string NormalizationH3C { get; set; }

		[JsonProperty("normalization.z.z")]
		string NormalizationZZ { get; set; }
	}

	public abstract class SimilarityBase : ISimilarity
	{
		/// <inheritdoc/>
		public abstract string Type { get; }

		/// <inheritdoc/>
		public string Normalization { get; set; }

		/// <inheritdoc/>
		public string NormalizationH1C { get; set; }

		/// <inheritdoc/>
		public string NormalizationH2C { get; set; }

		/// <inheritdoc/>
		public string NormalizationH3C { get; set; }

		/// <inheritdoc/>
		public string NormalizationZZ { get; set; }
	}
}
