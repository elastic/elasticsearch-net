using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	public class NodeIngestStats
	{
		/// <summary> Per pipeline ingest statistics </summary>
		[DataMember(Name = "pipelines")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, IngestStats>))]
		public IReadOnlyDictionary<string, IngestStats> Pipelines { get; internal set; }
			= EmptyReadOnly<string, IngestStats>.Dictionary;

		/// <summary> Overal global ingest statistics </summary>
		[DataMember(Name = "total")]
		public IngestStats Total { get; set; }
	}
}
