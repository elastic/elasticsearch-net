using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IFieldStatsResponse : IResponse
	{
		[JsonProperty("_shards")]
		ShardsMetaData Shards { get; }

		[JsonProperty("indices")]
		IReadOnlyDictionary<string, FieldStats> Indices { get; }
	}

	public class FieldStatsResponse : ResponseBase, IFieldStatsResponse
	{
		public ShardsMetaData Shards { get; internal set; }
		public IReadOnlyDictionary<string, FieldStats> Indices { get; internal set; } =
			EmptyReadOnly<string, FieldStats>.Dictionary;
	}

	[JsonObject]
	public class FieldStats
	{
		[JsonProperty("fields")]
		public IReadOnlyDictionary<string, FieldStatsField> Fields { get; internal set; } = EmptyReadOnly<string, FieldStatsField>.Dictionary;
	}

	public class FieldStatsField
	{
		[JsonProperty("max_doc")]
		public long MaxDoc { get; internal set; }

		[JsonProperty("doc_count")]
		public long DocCount { get; internal set; }

		[JsonProperty("density")]
		public long Density { get; internal set; }

		[JsonProperty("sum_doc_freq")]
		public long SumDocumentFrequency { get; internal set; }

		[JsonProperty("sum_total_term_freq")]
		public long SumTotalTermFrequency { get; internal set; }

		[JsonProperty("searchable")]
		public bool Searchable { get; internal set; }

		[JsonProperty("aggregatable")]
		public bool Aggregatable { get; internal set; }

		[JsonProperty("min_value")]
		//TODO this can also be an object in the case of geo_point/shape
		public object MinValue { get; internal set; }

		[JsonProperty("min_value_as_string")]
		public string MinValueAsString { get; internal set; }

		[JsonProperty("max_value")]
		//TODO this can also be an object in the case of geo_point/shape
		public object MaxValue { get; internal set; }

		[JsonProperty("max_value_as_string")]
		public string MaxValueAsString { get; internal set; }

	}
}
