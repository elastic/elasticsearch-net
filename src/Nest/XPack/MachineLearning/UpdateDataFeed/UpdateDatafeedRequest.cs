using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Updates a datafeed for a machine learning job.
	/// </summary>
	[MapsApi("ml.update_datafeed.json")]
	public partial interface IUpdateDatafeedRequest
	{
		/// <summary>
		/// If set, the datafeed performs aggregation searches.
		/// </summary>
		[DataMember(Name ="aggregations")]
		AggregationDictionary Aggregations { get; set; }

		/// <summary>
		/// Specifies how data searches are split into time chunks.
		/// </summary>
		[DataMember(Name ="chunking_config")]
		IChunkingConfig ChunkingConfig { get; set; }

		/// <summary>
		/// The interval at which scheduled queries are made while the datafeed runs in real time.
		/// The default value is either the bucket span for short bucket spans, or, for longer bucket spans,
		/// a sensible fraction of the bucket span.
		/// </summary>
		[DataMember(Name ="frequency")]
		Time Frequency { get; set; }

		///<summary>A list of index names to search within, wildcards are supported.</summary>
		[DataMember(Name ="indices")]
		[JsonFormatter(typeof(IndicesFormatter))]
		Indices Indices { get; set; }

		/// <summary>
		/// A numerical character string that uniquely identifies the job.
		/// </summary>
		[DataMember(Name ="job_id")]
		Id JobId { get; set; }

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda.
		/// </summary>
		[DataMember(Name ="query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// The number of seconds behind real time that data is queried.
		/// For example, if data from 10:04 a.m. might not be searchable until 10:06 a.m.,
		/// set this property to 120 seconds. The default value is 60s.
		/// </summary>
		[DataMember(Name ="query_delay")]
		Time QueryDelay { get; set; }

		/// <summary>
		/// Specifies scripts that evaluate custom expressions and returns script fields to the datafeed.
		/// The detector configuration in a job can contain functions that use these script fields.
		/// </summary>
		[DataMember(Name ="script_fields")]
		IScriptFields ScriptFields { get; set; }

		/// <summary>
		/// The size parameter used in searches.
		/// </summary>
		[DataMember(Name ="scroll_size")]
		int? ScrollSize { get; set; }
	}

	/// <inheritdoc />
	public partial class UpdateDatafeedRequest
	{
		/// <inheritdoc />
		public AggregationDictionary Aggregations { get; set; }

		/// <inheritdoc />
		public IChunkingConfig ChunkingConfig { get; set; }

		/// <inheritdoc />
		public Time Frequency { get; set; }

		/// <inheritdoc />
		public Indices Indices { get; set; }

		/// <inheritdoc />
		public Id JobId { get; set; }

		/// <inheritdoc />
		public QueryContainer Query { get; set; }

		/// <inheritdoc />
		public Time QueryDelay { get; set; }

		/// <inheritdoc />
		public IScriptFields ScriptFields { get; set; }

		/// <inheritdoc />
		public int? ScrollSize { get; set; }
	}

	public partial class UpdateDatafeedDescriptor<T> where T : class
	{
		AggregationDictionary IUpdateDatafeedRequest.Aggregations { get; set; }
		IChunkingConfig IUpdateDatafeedRequest.ChunkingConfig { get; set; }
		Time IUpdateDatafeedRequest.Frequency { get; set; }
		Indices IUpdateDatafeedRequest.Indices { get; set; }
		Id IUpdateDatafeedRequest.JobId { get; set; }
		QueryContainer IUpdateDatafeedRequest.Query { get; set; }
		Time IUpdateDatafeedRequest.QueryDelay { get; set; }
		IScriptFields IUpdateDatafeedRequest.ScriptFields { get; set; }
		int? IUpdateDatafeedRequest.ScrollSize { get; set; }

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector) =>
			Assign(a => a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<T>())?.Aggregations);

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> ChunkingConfig(Func<ChunkingConfigDescriptor, IChunkingConfig> selector) =>
			Assign(a => a.ChunkingConfig = selector.InvokeOrDefault(new ChunkingConfigDescriptor()));

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> Frequency(Time frequency) => Assign(a => a.Frequency = frequency);

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> Indices(Indices indices) => Assign(a => a.Indices = indices);

		///<summary>a shortcut into calling Indices(typeof(TOther))</summary>
		public UpdateDatafeedDescriptor<T> Indices<TOther>() => Assign(a => a.Indices = typeof(TOther));

		///<summary>A shortcut into calling Indices(Indices.All)</summary>
		public UpdateDatafeedDescriptor<T> AllIndices() => Indices(Nest.Indices.All);

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> JobId(Id jobId) => Assign(a => a.JobId = jobId);

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(a => a.Query = query?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> QueryDelay(Time queryDelay) => Assign(a => a.QueryDelay = queryDelay);

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(a => a.ScriptFields = selector?.Invoke(new ScriptFieldsDescriptor())?.Value);

		/// <inheritdoc />
		public UpdateDatafeedDescriptor<T> ScrollSize(int? scrollSize) => Assign(a => a.ScrollSize = scrollSize);
	}
}
