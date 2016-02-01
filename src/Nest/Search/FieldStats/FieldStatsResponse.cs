using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IFieldStatsResponse : IResponse
	{
		[JsonProperty("_shards")]
		ShardsMetaData Shards { get; set; }

		[JsonProperty("indices")]
		Dictionary<string, FieldStats> Indices { get; set; }
	}

	public class FieldStatsResponse : ResponseBase, IFieldStatsResponse
	{
		public ShardsMetaData Shards { get; set; }
		public Dictionary<string, FieldStats> Indices { get; set; }
	}

	[JsonObject]
	public class FieldStats
	{
		[JsonProperty("fields")]
		public Dictionary<string, FieldStatsField> Fields { get; set; }
	}

	public class FieldStatsField
	{
		[JsonProperty("max_doc")]
		public long MaxDoc { get; set; }

		[JsonProperty("doc_count")]
		public long DocCount { get; set; }

		[JsonProperty("density")]
		public long Density { get; set; }

		[JsonProperty("sum_doc_freq")]
		public long SumDocumentFrequency { get; set; }

		[JsonProperty("sum_total_term_freq")]
		public long SumTotalTermFrequency { get; set; }

		[JsonProperty("min_value")]
		public string MinValue { get; set; }

		[JsonProperty("max_value")]
		public string MaxValue { get; set; }
	}
}
