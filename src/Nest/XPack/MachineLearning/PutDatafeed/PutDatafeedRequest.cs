// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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

	public partial class PutDatafeedDescriptor<TDocument> where TDocument : class
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
		public PutDatafeedDescriptor<TDocument> Aggregations(Func<AggregationContainerDescriptor<TDocument>, IAggregationContainer> aggregationsSelector) =>
			Assign(aggregationsSelector(new AggregationContainerDescriptor<TDocument>())?.Aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc />
		public PutDatafeedDescriptor<TDocument> ChunkingConfig(Func<ChunkingConfigDescriptor, IChunkingConfig> selector) =>
			Assign(selector, (a, v) => a.ChunkingConfig = v?.Invoke(new ChunkingConfigDescriptor()));

		/// <inheritdoc />
		public PutDatafeedDescriptor<TDocument> Frequency(Time frequency) => Assign(frequency, (a, v) => a.Frequency = v);

		/// <inheritdoc />
		public PutDatafeedDescriptor<TDocument> Indices(Indices indices) => Assign(indices, (a, v) => a.Indices = v);

		///<summary>a shortcut into calling Indices(typeof(TOther))</summary>
		public PutDatafeedDescriptor<TDocument> Indices<TOther>() => Assign(typeof(TOther), (a, v) => a.Indices = v);

		///<summary>A shortcut into calling Indices(Indices.All)</summary>
		public PutDatafeedDescriptor<TDocument> AllIndices() => Indices(Nest.Indices.All);

		/// <inheritdoc />
		public PutDatafeedDescriptor<TDocument> JobId(Id jobId) => Assign(jobId, (a, v) => a.JobId = v);

		/// <inheritdoc />
		public PutDatafeedDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TDocument>()));

		/// <inheritdoc />
		public PutDatafeedDescriptor<TDocument> QueryDelay(Time queryDelay) => Assign(queryDelay, (a, v) => a.QueryDelay = v);

		/// <inheritdoc />
		public PutDatafeedDescriptor<TDocument> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(selector, (a, v) => a.ScriptFields = v?.Invoke(new ScriptFieldsDescriptor())?.Value);

		/// <inheritdoc />
		public PutDatafeedDescriptor<TDocument> ScrollSize(int? scrollSize) => Assign(scrollSize, (a, v) => a.ScrollSize = v);

	}
}
