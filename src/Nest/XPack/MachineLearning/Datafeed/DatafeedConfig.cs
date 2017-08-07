using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Configuration of a Machine Learning Datafeed
	/// </summary>
	[JsonObject]
	public class DatafeedConfig
	{
		/// <summary>
		/// The datafeed id
		/// </summary>
		[JsonProperty("datafeed_id")]
		public string DatafeedId { get; internal set; }

		/// <summary>
		/// the aggregation searches to perform for the datafeed
		/// </summary>
		[JsonProperty("aggregations")]
		public AggregationDictionary Aggregations { get; internal set; }

		/// <summary>
		/// Specifies how data searches are split into time chunks.
		/// </summary>
		[JsonProperty("chunking_config")]
		public IChunkingConfig ChunkingConfig { get; internal set; }

		/// <summary>
		/// The interval at which scheduled queries are made while the datafeed runs in real time.
		/// The default value is either the bucket span for short bucket spans, or, for longer bucket spans,
		/// a sensible fraction of the bucket span.
		/// </summary>
		[JsonProperty("frequency")]
		public Time Frequency { get; internal set; }

		///<summary>A list of index names to search within. Wildcards are supported</summary>
		[JsonProperty("indices")]
		[JsonConverter(typeof(IndicesJsonConverter))]
		public Indices Indices { get; internal set; }

		/// <summary>
		/// A numerical character string that uniquely identifies the job.
		/// </summary>
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		[JsonProperty("query")]
		public QueryContainer Query { get; internal set; }

		/// <summary>
		/// The number of seconds behind real time that data is queried.
		/// For example, if data from 10:04 A.M. might not be searchable in Elasticsearch until 10:06 A.M.,
		/// set this property to 120 seconds. The default value is 60s.
		/// </summary>
		[JsonProperty("query_delay")]
		public Time QueryDelay { get; internal set; }

		/// <summary>
		/// Specifies scripts that evaluate custom expressions and returns script fields to the datafeed. T
		/// he detector configuration in a job can contain functions that use these script fields.
		/// </summary>
		[JsonProperty("script_fields")]
		public IScriptFields ScriptFields { get; internal set; }

		/// <summary>
		/// The size parameter that is used in Elasticsearch searches
		/// </summary>
		[JsonProperty("scroll_size")]
		public int? ScrollSize { get; internal set; }

		///<summary>A list of types to search for within the specified indices</summary>
		[JsonProperty("types")]
		[JsonConverter(typeof(TypesJsonConverter))]
		public Types Types { get; internal set; }
	}
}
