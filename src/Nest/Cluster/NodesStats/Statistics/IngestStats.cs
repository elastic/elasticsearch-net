using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IngestStats
	{
		/// <summary> The total number of document ingested during the lifetime of this node</summary>
		[JsonProperty("count")]
		public long Count { get; set; }

		/// <summary> The total number of documents currently being ingested. </summary>
		[JsonProperty("current")]
		public long Current { get; set; }

		/// <summary> The total number ingest preprocessing operations failed during the lifetime of this node </summary>
		[JsonProperty("failed")]
		public long Failed { get; set; }

		/// <summary> The total time spent on ingest preprocessing documents during the lifetime of this node </summary>
		[JsonProperty("time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[JsonProperty("processors")]
		public IReadOnlyCollection<KeyedProcessorStats> Processors { get; internal set; } =
			EmptyReadOnly<KeyedProcessorStats>.Collection;
	}

	[JsonConverter(typeof(KeyValueJsonConverter<KeyedProcessorStats, ProcessStats>))]
	public class KeyedProcessorStats
	{
		/// <summary> The type of the processor </summary>
		public string Type { get; set; }

		/// <summary>The statistics for this processor</summary>
		public ProcessStats Statistics { get; set; }
	}

	[JsonObject]
	public class ProcessorStats
	{
		/// <summary> The total number of document ingested during the lifetime of this node </summary>
		[JsonProperty("count")]
		public long Count { get; internal set; }

		/// <summary> The total number of documents currently being ingested. </summary>
		[JsonProperty("current")]
		public long Current { get; internal set; }

		/// <summary> The total number ingest preprocessing operations failed during the lifetime of this node </summary>
		[JsonProperty("failed")]
		public long Failed { get; internal set; }

		/// <summary> The total time spent on ingest preprocessing documents during the lifetime of this node </summary>
		[JsonProperty("time_in_millis")]
		public long TimeInMilliseconds { get; internal set; }
	}
}
