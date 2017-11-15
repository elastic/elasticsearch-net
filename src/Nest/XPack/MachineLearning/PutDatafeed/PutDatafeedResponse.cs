using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The response from creating a datafeed.
	/// </summary>
	public partial interface IPutDatafeedResponse : IResponse
	{
		/// <summary>
		/// The datafeed id.
		/// </summary>
		[JsonProperty("datafeed_id")]
		string DatafeedId { get; }

		/// <summary>
		/// The aggregation searches to perform for the datafeed.
		/// </summary>
		[JsonProperty("aggregations")]
		AggregationDictionary Aggregations { get; }

		/// <summary>
		/// Specifies how data searches are split into time chunks.
		/// </summary>
		[JsonProperty("chunking_config")]
		IChunkingConfig ChunkingConfig { get; }

		/// <summary>
		/// The interval at which scheduled queries are made while the datafeed runs in real time.
		/// The default value is either the bucket span for short bucket spans, or, for longer bucket spans,
		/// a sensible fraction of the bucket span.
		/// </summary>
		[JsonProperty("frequency")]
		Time Frequency { get; }

		///<summary>
		/// A list of index names to search within, wildcards are supported.
		/// </summary>
		[JsonProperty("indices")]
		[JsonConverter(typeof(IndicesJsonConverter))]
		Indices Indices { get; }

		/// <summary>
		/// A numerical character string that uniquely identifies the job.
		/// </summary>
		[JsonProperty("job_id")]
		string JobId { get;  }

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		[JsonProperty("query")]
		QueryContainer Query { get; }

		/// <summary>
		/// The number of seconds behind real time that data is queried.
		/// For example, if data from 10:04 a.m. might not be searchable until 10:06 a.m.,
		/// set this property to 120 seconds. The default value is 60s.
		/// </summary>
		[JsonProperty("query_delay")]
		Time QueryDelay { get; }

		/// <summary>
		/// Specifies scripts that evaluate custom expressions and returns script fields to the datafeed. T
		/// The detector configuration in a job can contain functions that use these script fields.
		/// </summary>
		[JsonProperty("script_fields")]
		IScriptFields ScriptFields { get; }

		/// <summary>
		/// The size parameter that used in searches
		/// </summary>
		[JsonProperty("scroll_size")]
		int? ScrollSize { get; }

		///<summary>A list of types to search for within the specified indices</summary>
		[JsonProperty("types")]
		[JsonConverter(typeof(TypesJsonConverter))]
		Types Types { get;  }
	}

	/// <inheritdoc />
	public class PutDatafeedResponse : ResponseBase, IPutDatafeedResponse
	{
		/// <inheritdoc />
		public string DatafeedId { get; internal set; }
		/// <inheritdoc />
		public AggregationDictionary Aggregations { get; internal set; }
		/// <inheritdoc />
		public IChunkingConfig ChunkingConfig { get; internal set; }
		/// <inheritdoc />
		public Time Frequency { get; internal set; }
		/// <inheritdoc />
		public Indices Indices { get; internal set; }
		/// <inheritdoc />
		public string JobId { get; internal set; }
		/// <inheritdoc />
		public QueryContainer Query { get; internal set; }
		/// <inheritdoc />
		public Time QueryDelay { get; internal set; }
		/// <inheritdoc />
		public IScriptFields ScriptFields { get; internal set; }
		/// <inheritdoc />
		public int? ScrollSize { get; internal set; }
		/// <inheritdoc />
		public Types Types { get; internal set; }
	}
}
