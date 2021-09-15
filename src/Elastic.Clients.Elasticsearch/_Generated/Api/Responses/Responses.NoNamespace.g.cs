// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using OneOf;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch
{
	public partial class BulkResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("errors")]
		public bool Errors
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("ingest_took")]
		public long? IngestTook
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("items")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Global.Bulk.ResponseItemContainer> Items
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public long Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ClearScrollResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("num_freed")]
		public int NumFreed
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("succeeded")]
		public bool Succeeded
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ClosePointInTimeResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("num_freed")]
		public int NumFreed
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("succeeded")]
		public bool Succeeded
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class CountResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("count")]
		public long Count
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_shards")]
		public Elastic.Clients.Elasticsearch.ShardStatistics Shards
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class CreateResponse : WriteResponseBase
	{
	}

	public partial class DeleteByQueryResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("batches")]
		public long? Batches
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("deleted")]
		public long? Deleted
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("failures")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>? Failures
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("noops")]
		public long? Noops
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("requests_per_second")]
		public float? RequestsPerSecond
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("retries")]
		public Elastic.Clients.Elasticsearch.Retries? Retries
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("slice_id")]
		public int? SliceId
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("task")]
		public Elastic.Clients.Elasticsearch.TaskId? Task
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("throttled_millis")]
		public long? ThrottledMillis
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("throttled_until_millis")]
		public long? ThrottledUntilMillis
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("timed_out")]
		public bool? TimedOut
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public long? Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("total")]
		public long? Total
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("version_conflicts")]
		public long? VersionConflicts
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class DeleteByQueryRethrottleResponse : Tasks.ListResponse
	{
	}

	public partial class DeleteResponse : WriteResponseBase
	{
	}

	public partial class DeleteScriptResponse : AcknowledgedResponseBase
	{
	}

	public partial class ExistsResponse : ExistsResponseBase
	{
	}

	public partial class ExistsSourceResponse : ResponseBase
	{
	}

	public partial class ExplainResponse<TDocument> : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("explanation")]
		public Elastic.Clients.Elasticsearch.Global.Explain.ExplanationDetail? Explanation
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("get")]
		public Elastic.Clients.Elasticsearch.InlineGet<TDocument>? Get
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_id")]
		public Elastic.Clients.Elasticsearch.Id Id
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_index")]
		public Elastic.Clients.Elasticsearch.IndexName Index
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("matched")]
		public bool Matched
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_type")]
		public Elastic.Clients.Elasticsearch.DocType? Type
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class FieldCapsResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("fields")]
		public Dictionary<Elastic.Clients.Elasticsearch.Field, Dictionary<string, Elastic.Clients.Elasticsearch.Global.FieldCaps.FieldCapability>> Fields
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("indices")]
		public Elastic.Clients.Elasticsearch.Indices Indices
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class GetResponse<TDocument> : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("fields")]
		public Dictionary<string, object>? Fields
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("found")]
		public bool Found
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_id")]
		public Elastic.Clients.Elasticsearch.Id Id
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_index")]
		public Elastic.Clients.Elasticsearch.IndexName Index
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_primary_term")]
		public long? PrimaryTerm
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_routing")]
		public string? Routing
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_seq_no")]
		public Elastic.Clients.Elasticsearch.SequenceNumber? SeqNo
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_source")]
		public TDocument? Source
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_type")]
		public Elastic.Clients.Elasticsearch.DocType? Type
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_version")]
		public Elastic.Clients.Elasticsearch.VersionNumber? Version
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class GetScriptContextResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("contexts")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Global.GetScriptContext.Context> Contexts
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class GetScriptLanguagesResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("language_contexts")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Global.GetScriptLanguages.LanguageContext> LanguageContexts
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("types_allowed")]
		public IReadOnlyCollection<string> TypesAllowed
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class GetScriptResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("found")]
		public bool Found
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_id")]
		public Elastic.Clients.Elasticsearch.Id Id
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("script")]
		public Elastic.Clients.Elasticsearch.IStoredScript? Script
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class GetSourceResponse<TDocument> : ResponseBase
	{
	}

	public partial class IndexResponse : WriteResponseBase
	{
	}

	public partial class InfoResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("cluster_name")]
		public Elastic.Clients.Elasticsearch.Name ClusterName
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("cluster_uuid")]
		public Elastic.Clients.Elasticsearch.Uuid ClusterUuid
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("name")]
		public Elastic.Clients.Elasticsearch.Name Name
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("tagline")]
		public string Tagline
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("version")]
		public Elastic.Clients.Elasticsearch.ElasticsearchVersionInfo Version
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class MgetResponse<TDocument> : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("docs")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Global.Mget.Hit<TDocument>> Docs
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class MsearchResponse<TDocument> : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("responses")]
		public IReadOnlyCollection<Union<Elastic.Clients.Elasticsearch.Global.Msearch.SearchResult<TDocument>, Elastic.Clients.Elasticsearch.ErrorResponseBase>> Responses
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public long Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class MsearchTemplateResponse<TDocument> : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("responses")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.SearchResponse<TDocument>> Responses
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public long Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class MtermvectorsResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("docs")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Global.Mtermvectors.TermVectorsResult> Docs
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class OpenPointInTimeResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("id")]
		public Elastic.Clients.Elasticsearch.Id Id
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class PingResponse : ResponseBase
	{
	}

	public partial class PutScriptResponse : AcknowledgedResponseBase
	{
	}

	public partial class RankEvalResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("details")]
		public Dictionary<Elastic.Clients.Elasticsearch.Id, Elastic.Clients.Elasticsearch.Global.RankEval.RankEvalMetricDetail> Details
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("failures")]
		public Dictionary<string, object> Failures
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("metric_score")]
		public double MetricScore
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ReindexResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("batches")]
		public long? Batches
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("created")]
		public long? Created
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("deleted")]
		public long? Deleted
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("failures")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>? Failures
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("noops")]
		public long? Noops
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("requests_per_second")]
		public long? RequestsPerSecond
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("retries")]
		public Elastic.Clients.Elasticsearch.Retries? Retries
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("slice_id")]
		public int? SliceId
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("task")]
		public Elastic.Clients.Elasticsearch.TaskId? Task
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("throttled_millis")]
		public Elastic.Clients.Elasticsearch.EpochMillis? ThrottledMillis
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("throttled_until_millis")]
		public Elastic.Clients.Elasticsearch.EpochMillis? ThrottledUntilMillis
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("timed_out")]
		public bool? TimedOut
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public Elastic.Clients.Elasticsearch.Time? Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("total")]
		public long? Total
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("updated")]
		public long? Updated
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("version_conflicts")]
		public long? VersionConflicts
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ReindexRethrottleResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("nodes")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.Global.ReindexRethrottle.ReindexNode> Nodes
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class RenderSearchTemplateResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("template_output")]
		public Dictionary<string, object> TemplateOutput
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ScriptsPainlessExecuteResponse<TResult> : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("result")]
		public TResult Result
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ScrollResponse<TDocument> : SearchResponse<TDocument>
	{
	}

	public partial class SearchMvtResponse : ResponseBase
	{
	}

	public partial class SearchResponse<TDocument> : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("aggregations")]
		public Dictionary<Elastic.Clients.Elasticsearch.AggregateName, Elastic.Clients.Elasticsearch.Aggregations.Aggregate>? Aggregations
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_clusters")]
		public Elastic.Clients.Elasticsearch.ClusterStatistics? Clusters
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("documents")]
		public IReadOnlyCollection<TDocument>? Documents
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("fields")]
		public Dictionary<string, object>? Fields
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("hits")]
		public Elastic.Clients.Elasticsearch.Global.Search.HitsMetadata<TDocument> Hits
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("max_score")]
		public double? MaxScore
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("num_reduce_phases")]
		public long? NumReducePhases
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("pit_id")]
		public Elastic.Clients.Elasticsearch.Id? PitId
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("profile")]
		public Elastic.Clients.Elasticsearch.Global.Search.Profile? Profile
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_scroll_id")]
		public Elastic.Clients.Elasticsearch.ScrollId? ScrollId
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_shards")]
		public Elastic.Clients.Elasticsearch.ShardStatistics Shards
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("suggest")]
		public Dictionary<Elastic.Clients.Elasticsearch.SuggestionName, IReadOnlyCollection<Elastic.Clients.Elasticsearch.Global.Search.Suggest<TDocument>>>? Suggest
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("terminated_early")]
		public bool? TerminatedEarly
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("timed_out")]
		public bool TimedOut
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public long Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class SearchShardsResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("indices")]
		public Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.Global.SearchShards.ShardStoreIndex> Indices
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("nodes")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.NodeAttributes> Nodes
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("shards")]
		public IReadOnlyCollection<IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeShard>> Shards
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class SearchTemplateResponse<TDocument> : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("hits")]
		public Elastic.Clients.Elasticsearch.Global.Search.HitsMetadata<TDocument> Hits
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_shards")]
		public Elastic.Clients.Elasticsearch.ShardStatistics Shards
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("timed_out")]
		public bool TimedOut
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public int Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class TermsEnumResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("complete")]
		public bool Complete
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_shards")]
		public Elastic.Clients.Elasticsearch.ShardStatistics Shards
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("terms")]
		public IReadOnlyCollection<string> Terms
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class TermvectorsResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("found")]
		public bool Found
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_id")]
		public Elastic.Clients.Elasticsearch.Id Id
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_index")]
		public Elastic.Clients.Elasticsearch.IndexName Index
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("term_vectors")]
		public Dictionary<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.Global.Termvectors.TermVector>? TermVectors
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public long Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_type")]
		public Elastic.Clients.Elasticsearch.DocType? Type
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("_version")]
		public Elastic.Clients.Elasticsearch.VersionNumber Version
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class UpdateByQueryResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("batches")]
		public long? Batches
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("deleted")]
		public long? Deleted
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("failures")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>? Failures
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("noops")]
		public long? Noops
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("requests_per_second")]
		public float? RequestsPerSecond
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("retries")]
		public Elastic.Clients.Elasticsearch.Retries? Retries
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("task")]
		public Elastic.Clients.Elasticsearch.TaskId? Task
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("throttled_millis")]
		public ulong? ThrottledMillis
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("throttled_until_millis")]
		public ulong? ThrottledUntilMillis
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("timed_out")]
		public bool? TimedOut
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("took")]
		public long? Took
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("total")]
		public long? Total
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("updated")]
		public long? Updated
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}

		[JsonInclude]
		[JsonPropertyName("version_conflicts")]
		public long? VersionConflicts
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class UpdateByQueryRethrottleResponse : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("nodes")]
		public Dictionary<string, Elastic.Clients.Elasticsearch.Global.UpdateByQueryRethrottle.UpdateByQueryRethrottleNode> Nodes
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}

	public partial class UpdateResponse<TDocument> : WriteResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("get")]
		public Elastic.Clients.Elasticsearch.InlineGet<TDocument>? Get
		{
			get;
#if NET5_0_OR_GREATER
			init;
#else
			internal set;
#endif
		}
	}
}