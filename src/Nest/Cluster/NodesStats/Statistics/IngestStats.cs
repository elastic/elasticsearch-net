// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	public class IngestStats
	{
		/// <summary>
		/// The total number of document ingested during the lifetime of this node
		/// </summary>
		[DataMember(Name ="count")]
		public long Count { get; set; }

		/// <summary>
		/// The total number of documents currently being ingested.
		/// </summary>
		[DataMember(Name ="current")]
		public long Current { get; set; }

		/// <summary>
		/// The total number ingest preprocessing operations failed during the lifetime of this node
		/// </summary>
		[DataMember(Name ="failed")]
		public long Failed { get; set; }

		/// <summary>
		/// The total time spent on ingest preprocessing documents during the lifetime of this node
		/// </summary>
		[DataMember(Name ="time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		[DataMember(Name = "processors")]
		public IReadOnlyCollection<KeyedProcessorStats> Processors { get; internal set; } =
			EmptyReadOnly<KeyedProcessorStats>.Collection;
	}

	[JsonFormatter(typeof(KeyedProcessorStatsFormatter))]
	public class KeyedProcessorStats
	{
		/// <summary> The type of the processor </summary>
		public string Type { get; set; }

		/// <summary>The statistics for this processor</summary>
		public ProcessStats Statistics { get; set; }
	}

	internal class KeyedProcessorStatsFormatter : IJsonFormatter<KeyedProcessorStats>
	{
		public KeyedProcessorStats Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var count = 0;
			var stats = new KeyedProcessorStats();
			while (reader.ReadIsInObject(ref count))
			{
				stats.Type = reader.ReadPropertyName();
				stats.Statistics = formatterResolver.GetFormatter<ProcessStats>()
					.Deserialize(ref reader, formatterResolver);
			}

			return stats;
		}

		public void Serialize(ref JsonWriter writer, KeyedProcessorStats value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Type == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			writer.WritePropertyName(value.Type);
			formatterResolver.GetFormatter<ProcessStats>().Serialize(ref writer, value.Statistics, formatterResolver);
			writer.WriteEndObject();
		}
	}

	public class ProcessorStats
	{
		/// <summary> The total number of document ingested during the lifetime of this node </summary>
		[DataMember(Name = "count")]
		public long Count { get; internal set; }

		/// <summary> The total number of documents currently being ingested. </summary>
		[DataMember(Name = "current")]
		public long Current { get; internal set; }

		/// <summary> The total number ingest preprocessing operations failed during the lifetime of this node </summary>
		[DataMember(Name = "failed")]
		public long Failed { get; internal set; }

		/// <summary> The total time spent on ingest preprocessing documents during the lifetime of this node </summary>
		[DataMember(Name = "time_in_millis")]
		public long TimeInMilliseconds { get; internal set; }
	}
}
