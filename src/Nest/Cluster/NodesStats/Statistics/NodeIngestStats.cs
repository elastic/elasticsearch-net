using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class NodeIngestStats
	{
		/// <summary> Per pipeline ingest statistics </summary>
		[JsonProperty("pipelines")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IngestStats>))]
		public IReadOnlyDictionary<string, IngestStats> Pipelines { get; internal set; }
			= EmptyReadOnly<string, IngestStats>.Dictionary;

		/// <summary> Overal global ingest statistics </summary>
		[JsonProperty("total")]
		public IngestStats Total { get; set; }
	}
}
