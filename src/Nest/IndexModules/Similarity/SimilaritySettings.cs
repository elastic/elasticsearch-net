using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	[JsonConverter(typeof(SimilaritySettingsJsonConverter))]
	public class SimilaritySettings
	{
		public SimilaritySettings()
		{
			this.CustomSimilarities = new Dictionary<string, SimilarityBase>();
		}

		public string Default { get; set; }

		[JsonConverter(typeof(SimilarityCollectionJsonConverter))]
		public IDictionary<string, SimilarityBase> CustomSimilarities { get; set; }
	}
}
