using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[Obsolete("Scheduled to be removed in 6.0")]
	public interface IFieldStatsResponse : IResponse
	{
		[JsonProperty("indices")]
		IReadOnlyDictionary<string, FieldStats> Indices { get; }

		[JsonProperty("_shards")]
		ShardsMetaData Shards { get; }
	}

	[Obsolete("Scheduled to be removed in 6.0")]
	public class FieldStatsResponse : ResponseBase, IFieldStatsResponse
	{
		public IReadOnlyDictionary<string, FieldStats> Indices { get; internal set; } =
			EmptyReadOnly<string, FieldStats>.Dictionary;

		public ShardsMetaData Shards { get; internal set; }
	}

	[JsonObject]
	[Obsolete("Scheduled to be removed in 6.0")]
	public class FieldStats
	{
		[JsonProperty("fields")]
		public IReadOnlyDictionary<string, FieldStatsField> Fields { get; internal set; } = EmptyReadOnly<string, FieldStatsField>.Dictionary;
	}

	[Obsolete("Scheduled to be removed in 6.0")]
	public class FieldStatsField
	{
		[JsonProperty("aggregatable")]
		public bool Aggregatable { get; internal set; }

		[JsonProperty("density")]
		public long Density { get; internal set; }

		[JsonProperty("doc_count")]
		public long DocCount { get; internal set; }

		[JsonProperty("max_doc")]
		public long MaxDoc { get; internal set; }

		/// <summary>
		/// Returns the max value of a field. In NEST 5.x this is always
		/// returned as a string which does not work for geo_point and geo_shape
		/// typed fields which return an object here since Elasticsearch 5.3
		/// so in 5.x this is always null for geo_point and geo_shape. Please use <see cref="MaxValueAsString" />
		/// </summary>
		[JsonProperty("max_value")]
		[JsonConverter(typeof(FieldMinMaxValueJsonConverter))]
		public string MaxValue { get; internal set; }

		[JsonProperty("max_value_as_string")]
		public string MaxValueAsString { get; internal set; }

		/// <summary>
		/// Returns the min value of a field. In NEST 5.x this is always
		/// returned as a string which does not work for geo_point and geo_shape
		/// typed fields which return an object here since Elasticsearch 5.3
		/// so in 5.x this is always null for geo_point and geo_shape. Please use <see cref="MinValueAsString" />
		/// </summary>
		[JsonProperty("min_value")]
		[JsonConverter(typeof(FieldMinMaxValueJsonConverter))]
		public string MinValue { get; internal set; }

		[JsonProperty("min_value_as_string")]
		public string MinValueAsString { get; internal set; }

		[JsonProperty("searchable")]
		public bool Searchable { get; internal set; }

		[JsonProperty("sum_doc_freq")]
		public long SumDocumentFrequency { get; internal set; }

		[JsonProperty("sum_total_term_freq")]
		public long SumTotalTermFrequency { get; internal set; }
	}
}
