
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	[JsonConverter(typeof(SimilaritySettingsConverter))]
	public class SimilaritySettings
	{
		public SimilaritySettings()
		{
			this.CustomSimilarities = new Dictionary<string, SimilarityBase>();
		}

		public string Default { get; set; }

		[JsonConverter(typeof(SimilarityCollectionConverter))]
		public IDictionary<string, SimilarityBase> CustomSimilarities { get; set; }
	}
}
