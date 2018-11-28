using System;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// Creates a datafeed for a machine learning job.
	/// </summary>
	[MapsApi("ml.put_datafeed.json")]
	public partial interface IPutDatafeedRequest
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

		/// <summary>
		///  A list of index names to search within, wildcards are supported.
		/// </summary>
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
		/// The size parameter that is used in Elasticsearch searches.
		/// </summary>
		[DataMember(Name ="scroll_size")]
		int? ScrollSize { get; set; }
	}

	/// <inheritdoc />
	public partial class PutDatafeedRequest
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

	public partial class PutDatafeedDescriptor<T> where T : class
	{
		AggregationDictionary IPutDatafeedRequest.Aggregations { get; set; }
		IChunkingConfig IPutDatafeedRequest.ChunkingConfig { get; set; }
		Time IPutDatafeedRequest.Frequency { get; set; }
		Indices IPutDatafeedRequest.Indices { get; set; }
		Id IPutDatafeedRequest.JobId { get; set; }
		QueryContainer IPutDatafeedRequest.Query { get; set; }
		Time IPutDatafeedRequest.QueryDelay { get; set; }
		IScriptFields IPutDatafeedRequest.ScriptFields { get; set; }
		int? IPutDatafeedRequest.ScrollSize { get; set; }

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector) =>
			Assign(a => a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<T>())?.Aggregations);

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> ChunkingConfig(Func<ChunkingConfigDescriptor, IChunkingConfig> selector) =>
			Assign(a => a.ChunkingConfig = selector?.Invoke(new ChunkingConfigDescriptor()));

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> Frequency(Time frequency) => Assign(a => a.Frequency = frequency);

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> Indices(Indices indices) => Assign(a => a.Indices = indices);

		///<summary>a shortcut into calling Indices(typeof(TOther))</summary>
		public PutDatafeedDescriptor<T> Indices<TOther>() => Assign(a => a.Indices = typeof(TOther));

		///<summary>A shortcut into calling Indices(Indices.All)</summary>
		public PutDatafeedDescriptor<T> AllIndices() => Indices(Nest.Indices.All);

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> JobId(Id jobId) => Assign(a => a.JobId = jobId);

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(a => a.Query = query?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> QueryDelay(Time queryDelay) => Assign(a => a.QueryDelay = queryDelay);

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(a => a.ScriptFields = selector?.Invoke(new ScriptFieldsDescriptor())?.Value);

		/// <inheritdoc />
		public PutDatafeedDescriptor<T> ScrollSize(int? scrollSize) => Assign(a => a.ScrollSize = scrollSize);

	}
}
