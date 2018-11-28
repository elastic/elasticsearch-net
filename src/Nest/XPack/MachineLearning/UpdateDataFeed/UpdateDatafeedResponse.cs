using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// The response from updating a datafeed
	/// </summary>
	public partial interface IUpdateDatafeedResponse : IResponse
	{
		/// <summary>
		/// The aggregation searches to perform for the datafeed.
		/// </summary>
		[DataMember(Name ="aggregations")]
		AggregationDictionary Aggregations { get; }

		/// <summary>
		/// Specifies how data searches are split into time chunks.
		/// </summary>
		[DataMember(Name ="chunking_config")]
		IChunkingConfig ChunkingConfig { get; }

		/// <summary>
		/// The datafeed id.
		/// </summary>
		[DataMember(Name ="datafeed_id")]
		string DatafeedId { get; }

		/// <summary>
		/// The interval at which scheduled queries are made while the datafeed runs in real time.
		/// The default value is either the bucket span for short bucket spans, or, for longer bucket spans,
		/// a sensible fraction of the bucket span.
		/// </summary>
		[DataMember(Name ="frequency")]
		Time Frequency { get; }

		///<summary>A list of index names to search within, wildcards are supported.</summary>
		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(IndicesFormatter))]
		Indices Indices { get; }

		/// <summary>
		/// A numerical character string that uniquely identifies the job.
		/// </summary>
		[DataMember(Name ="job_id")]
		string JobId { get; }

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		[DataMember(Name ="query")]
		QueryContainer Query { get; }

		/// <summary>
		/// The number of seconds behind real time that data is queried.
		/// For example, if data from 10:04 a.m. might not be searchable until 10:06 a.m.,
		/// set this property to 120 seconds. The default value is 60s.
		/// </summary>
		[DataMember(Name ="query_delay")]
		Time QueryDelay { get; }

		/// <summary>
		/// Specifies scripts that evaluate custom expressions and returns script fields to the datafeed.
		/// The detector configuration in a job can contain functions that use these script fields.
		/// </summary>
		[DataMember(Name ="script_fields")]
		IScriptFields ScriptFields { get; }

		/// <summary>
		/// The size parameter that is used in Elasticsearch searches
		/// </summary>
		[DataMember(Name ="scroll_size")]
		int? ScrollSize { get; }
	}

	public class UpdateDatafeedResponse : ResponseBase, IUpdateDatafeedResponse
	{
		public AggregationDictionary Aggregations { get; internal set; }
		public IChunkingConfig ChunkingConfig { get; internal set; }
		public string DatafeedId { get; internal set; }
		public Time Frequency { get; internal set; }
		public Indices Indices { get; internal set; }
		public string JobId { get; internal set; }
		public QueryContainer Query { get; internal set; }
		public Time QueryDelay { get; internal set; }
		public IScriptFields ScriptFields { get; internal set; }
		public int? ScrollSize { get; internal set; }
	}
}
