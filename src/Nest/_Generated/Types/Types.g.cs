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

using Elastic.Transport.Products.Elasticsearch.Failures;
using OneOf;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Nest
{
	public partial class BulkIndexByScrollFailure
	{
		[JsonPropertyName("cause")]
		public Nest.MainError Cause
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("id")]
		public Nest.Id Id
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index")]
		public Nest.IndexName Index
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("status")]
		public int Status
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("type")]
		public string Type
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class BulkStats
	{
		[JsonPropertyName("avg_size")]
		public Nest.ByteSize? AvgSize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("avg_size_in_bytes")]
		public long AvgSizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("avg_time")]
		public string? AvgTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("avg_time_in_millis")]
		public long AvgTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_operations")]
		public long TotalOperations
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_size")]
		public Nest.ByteSize? TotalSize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_size_in_bytes")]
		public long TotalSizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time")]
		public string? TotalTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time_in_millis")]
		public long TotalTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ChainTransform
	{
		[JsonPropertyName("transforms")]
		public IEnumerable<Nest.TransformContainer> Transforms { get; set; }
	}

	public partial class ClusterStatistics
	{
		[JsonPropertyName("skipped")]
		public int Skipped
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("successful")]
		public int Successful
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total")]
		public int Total
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class CompletionStats
	{
		[JsonPropertyName("fields")]
		public Dictionary<Nest.Field, Nest.FieldSizeUsage>? Fields
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("size")]
		public Nest.ByteSize? Size
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("size_in_bytes")]
		public long SizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class DateField
	{
		[JsonPropertyName("field")]
		public Nest.Field Field { get; set; }

		[JsonPropertyName("format")]
		public string? Format { get; set; }

		[JsonPropertyName("include_unmapped")]
		public bool? IncludeUnmapped { get; set; }
	}

	public partial class DocStats
	{
		[JsonPropertyName("count")]
		public long Count
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("deleted")]
		public long Deleted
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ElasticsearchVersionInfo
	{
		[JsonPropertyName("build_date")]
		public Nest.DateString BuildDate
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("build_flavor")]
		public string BuildFlavor
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("build_hash")]
		public string BuildHash
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("build_snapshot")]
		public bool BuildSnapshot
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("build_type")]
		public string BuildType
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("lucene_version")]
		public Nest.VersionString LuceneVersion
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("minimum_index_compatibility_version")]
		public Nest.VersionString MinimumIndexCompatibilityVersion
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("minimum_wire_compatibility_version")]
		public Nest.VersionString MinimumWireCompatibilityVersion
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("number")]
		public string Number
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class EmptyObject
	{
	}

	public partial class ErrorCause
	{
		[JsonPropertyName("bytes_limit")]
		public long? BytesLimit
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("bytes_wanted")]
		public long? BytesWanted
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("caused_by")]
		public Nest.ErrorCause? CausedBy
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("col")]
		public int? Col
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("column")]
		public int? Column
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("failed_shards")]
		public IReadOnlyCollection<Nest.ShardFailure>? FailedShards
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("grouped")]
		public bool? Grouped
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("header")]
		public Nest.HttpHeaders? Header
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index")]
		public Nest.IndexName? Index
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_uuid")]
		public Nest.Uuid? IndexUuid
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("lang")]
		public string? Lang
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("language")]
		public string? Language
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("licensed_expired_feature")]
		public string? LicensedExpiredFeature
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("line")]
		public int? Line
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("max_buckets")]
		public int? MaxBuckets
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("phase")]
		public string? Phase
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("position")]
		public Nest.Global.ScriptsPainlessExecute.PainlessExecutionPosition? Position
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("processor_type")]
		public string? ProcessorType
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("property_name")]
		public string? PropertyName
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("reason")]
		public string Reason
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("resource_id")]
		public Nest.Ids? ResourceId
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("resource_type")]
		public string? ResourceType
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("root_cause")]
		public IReadOnlyCollection<Nest.ErrorCause>? RootCause
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("script")]
		public string? Script
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("script_stack")]
		public IReadOnlyCollection<string>? ScriptStack
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("shard")]
		public Union<int, string>? Shard
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("stack_trace")]
		public string? StackTrace
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("type")]
		public string Type
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class FieldMemoryUsage
	{
		[JsonPropertyName("memory_size")]
		public Nest.ByteSize? MemorySize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory_size_in_bytes")]
		public long MemorySizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class FieldSizeUsage
	{
		[JsonPropertyName("size")]
		public Nest.ByteSize? Size
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("size_in_bytes")]
		public long SizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class FielddataStats
	{
		[JsonPropertyName("evictions")]
		public long? Evictions
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("fields")]
		public Dictionary<Nest.Field, Nest.FieldMemoryUsage>? Fields
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory_size")]
		public Nest.ByteSize? MemorySize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory_size_in_bytes")]
		public long MemorySizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class FlushStats
	{
		[JsonPropertyName("periodic")]
		public long Periodic
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total")]
		public long Total
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time")]
		public string? TotalTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time_in_millis")]
		public long TotalTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class GetStats
	{
		[JsonPropertyName("current")]
		public long Current
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("exists_time")]
		public string? ExistsTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("exists_time_in_millis")]
		public long ExistsTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("exists_total")]
		public long ExistsTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("missing_time")]
		public string? MissingTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("missing_time_in_millis")]
		public long MissingTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("missing_total")]
		public long MissingTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("time")]
		public string? Time
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("time_in_millis")]
		public long TimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total")]
		public long Total
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class IndexedScript
	{
		[JsonPropertyName("id")]
		public Nest.Id Id
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class IndexingStats
	{
		[JsonPropertyName("delete_current")]
		public long DeleteCurrent
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("delete_time")]
		public string? DeleteTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("delete_time_in_millis")]
		public long DeleteTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("delete_total")]
		public long DeleteTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_current")]
		public long IndexCurrent
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_failed")]
		public long IndexFailed
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_time")]
		public string? IndexTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_time_in_millis")]
		public long IndexTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_total")]
		public long IndexTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("is_throttled")]
		public bool IsThrottled
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("noop_update_total")]
		public long NoopUpdateTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("throttle_time")]
		public string? ThrottleTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("throttle_time_in_millis")]
		public long ThrottleTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("types")]
		public Dictionary<string, Nest.IndexingStats>? Types
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class InlineGet<TDocument>
	{
		[JsonPropertyName("fields")]
		public Dictionary<string, object>? Fields
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("found")]
		public bool Found
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("_primary_term")]
		public long PrimaryTerm
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("_routing")]
		public Nest.Routing? Routing
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("_seq_no")]
		public Nest.SequenceNumber SeqNo
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("_source")]
		public TDocument Source
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class InlineScript
	{
		[JsonPropertyName("source")]
		public string Source { get; set; }
	}

	public partial class LatLon
	{
		[JsonPropertyName("lat")]
		public double Lat { get; set; }

		[JsonPropertyName("lon")]
		public double Lon { get; set; }
	}

	public partial class MainError : ErrorCause
	{
		[JsonPropertyName("headers")]
		public Dictionary<string, string>? Headers
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class MergesStats
	{
		[JsonPropertyName("current")]
		public long Current
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("current_docs")]
		public long CurrentDocs
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("current_size")]
		public string? CurrentSize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("current_size_in_bytes")]
		public long CurrentSizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total")]
		public long Total
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_auto_throttle")]
		public string? TotalAutoThrottle
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_auto_throttle_in_bytes")]
		public long TotalAutoThrottleInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_docs")]
		public long TotalDocs
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_size")]
		public string? TotalSize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_size_in_bytes")]
		public long TotalSizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_stopped_time")]
		public string? TotalStoppedTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_stopped_time_in_millis")]
		public long TotalStoppedTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_throttled_time")]
		public string? TotalThrottledTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_throttled_time_in_millis")]
		public long TotalThrottledTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time")]
		public string? TotalTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time_in_millis")]
		public long TotalTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class NodeAttributes
	{
		[JsonPropertyName("attributes")]
		public Dictionary<string, string> Attributes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("ephemeral_id")]
		public Nest.Id EphemeralId
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("id")]
		public Nest.Id? Id
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("name")]
		public Nest.NodeName Name
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("roles")]
		public Nest.NodeRoles? Roles
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("transport_address")]
		public Nest.TransportAddress TransportAddress
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class NodeShard
	{
		[JsonPropertyName("allocation_id")]
		public Dictionary<string, Nest.Id>? AllocationId
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index")]
		public Nest.IndexName Index
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("node")]
		public Nest.NodeName? Node
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("primary")]
		public bool Primary
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("recovery_source")]
		public Dictionary<string, Nest.Id>? RecoverySource
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("shard")]
		public int Shard
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("state")]
		public Nest.IndexManagement.Stats.ShardRoutingState State
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("unassigned_info")]
		public Nest.Cluster.AllocationExplain.UnassignedInformation? UnassignedInfo
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class NodeStatistics
	{
		[JsonPropertyName("failed")]
		public int Failed
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("failures")]
		public IReadOnlyCollection<Nest.ErrorCause>? Failures
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("successful")]
		public int Successful
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total")]
		public int Total
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class PluginStats
	{
		[JsonPropertyName("classname")]
		public string Classname
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("description")]
		public string Description
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("elasticsearch_version")]
		public Nest.VersionString ElasticsearchVersion
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("extended_plugins")]
		public IReadOnlyCollection<string> ExtendedPlugins
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("has_native_controller")]
		public bool HasNativeController
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("java_version")]
		public Nest.VersionString JavaVersion
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("licensed")]
		public bool Licensed
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("name")]
		public Nest.Name Name
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("type")]
		public string Type
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("version")]
		public Nest.VersionString Version
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class QueryCacheStats
	{
		[JsonPropertyName("cache_count")]
		public int CacheCount
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("cache_size")]
		public int CacheSize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("evictions")]
		public int Evictions
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("hit_count")]
		public int HitCount
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory_size")]
		public Nest.ByteSize? MemorySize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory_size_in_bytes")]
		public int MemorySizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("miss_count")]
		public int MissCount
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_count")]
		public int TotalCount
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class RecoveryStats
	{
		[JsonPropertyName("current_as_source")]
		public long CurrentAsSource
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("current_as_target")]
		public long CurrentAsTarget
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("throttle_time")]
		public string? ThrottleTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("throttle_time_in_millis")]
		public long ThrottleTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class RefreshStats
	{
		[JsonPropertyName("external_total")]
		public long ExternalTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("external_total_time_in_millis")]
		public long ExternalTotalTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("listeners")]
		public long Listeners
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total")]
		public long Total
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time")]
		public string? TotalTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time_in_millis")]
		public long TotalTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class RequestBase
	{
	}

	public partial class RequestCacheStats
	{
		[JsonPropertyName("evictions")]
		public long Evictions
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("hit_count")]
		public long HitCount
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory_size")]
		public string? MemorySize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory_size_in_bytes")]
		public long MemorySizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("miss_count")]
		public long MissCount
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class Retries
	{
		[JsonPropertyName("bulk")]
		public long Bulk
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("search")]
		public long Search
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ScriptField
	{
		[JsonPropertyName("ignore_failure")]
		public bool? IgnoreFailure { get; set; }

		[JsonPropertyName("script")]
		public Nest.Script Script { get; set; }
	}

	public partial class ScriptTransform
	{
		[JsonPropertyName("lang")]
		public string Lang { get; set; }

		[JsonPropertyName("params")]
		public Dictionary<string, object> Params { get; set; }
	}

	public partial class SearchStats
	{
		[JsonPropertyName("fetch_current")]
		public long FetchCurrent
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("fetch_time_in_millis")]
		public long FetchTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("fetch_total")]
		public long FetchTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("groups")]
		public Dictionary<string, Nest.SearchStats>? Groups
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("open_contexts")]
		public long? OpenContexts
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("query_current")]
		public long QueryCurrent
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("query_time_in_millis")]
		public long QueryTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("query_total")]
		public long QueryTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("scroll_current")]
		public long ScrollCurrent
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("scroll_time_in_millis")]
		public long ScrollTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("scroll_total")]
		public long ScrollTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("suggest_current")]
		public long SuggestCurrent
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("suggest_time_in_millis")]
		public long SuggestTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("suggest_total")]
		public long SuggestTotal
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class SearchTransform
	{
		[JsonPropertyName("request")]
		public Nest.Watcher.SearchInputRequestDefinition Request { get; set; }

		[JsonPropertyName("timeout")]
		public Nest.Time Timeout { get; set; }
	}

	public partial class SegmentsStats
	{
		[JsonPropertyName("count")]
		public int Count
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("doc_values_memory")]
		public Nest.ByteSize? DocValuesMemory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("doc_values_memory_in_bytes")]
		public int DocValuesMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("file_sizes")]
		public Dictionary<string, Nest.IndexManagement.Stats.ShardFileSizeInfo> FileSizes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("fixed_bit_set")]
		public Nest.ByteSize? FixedBitSet
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("fixed_bit_set_memory_in_bytes")]
		public int FixedBitSetMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_writer_max_memory_in_bytes")]
		public int? IndexWriterMaxMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_writer_memory")]
		public Nest.ByteSize? IndexWriterMemory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("index_writer_memory_in_bytes")]
		public int IndexWriterMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("max_unsafe_auto_id_timestamp")]
		public int MaxUnsafeAutoIdTimestamp
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory")]
		public Nest.ByteSize? Memory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("memory_in_bytes")]
		public int MemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("norms_memory")]
		public Nest.ByteSize? NormsMemory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("norms_memory_in_bytes")]
		public int NormsMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("points_memory")]
		public Nest.ByteSize? PointsMemory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("points_memory_in_bytes")]
		public int PointsMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("stored_fields_memory_in_bytes")]
		public int StoredFieldsMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("stored_memory")]
		public Nest.ByteSize? StoredMemory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("terms_memory")]
		public Nest.ByteSize? TermsMemory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("terms_memory_in_bytes")]
		public int TermsMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("term_vectors_memory_in_bytes")]
		public int TermVectorsMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("term_vectory_memory")]
		public Nest.ByteSize? TermVectoryMemory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("version_map_memory")]
		public Nest.ByteSize? VersionMapMemory
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("version_map_memory_in_bytes")]
		public int VersionMapMemoryInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ShardFailure
	{
		[JsonPropertyName("index")]
		public Nest.IndexName? Index
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("node")]
		public string? Node
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("reason")]
		public Nest.ErrorCause Reason
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("shard")]
		public int Shard
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("status")]
		public string? Status
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class ShardStatistics
	{
		[JsonPropertyName("failed")]
		public uint Failed
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("failures")]
		public IReadOnlyCollection<Nest.ShardFailure>? Failures
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("skipped")]
		public uint? Skipped
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("successful")]
		public uint Successful
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total")]
		public uint Total
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class SlicedScroll
	{
		[JsonPropertyName("field")]
		public Nest.Field? Field { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("max")]
		public int Max { get; set; }
	}

	public partial class StoreStats
	{
		[JsonPropertyName("reserved")]
		public Nest.ByteSize? Reserved
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("reserved_in_bytes")]
		public int ReservedInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("size")]
		public Nest.ByteSize? Size
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("size_in_bytes")]
		public int SizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_data_set_size")]
		public Nest.ByteSize? TotalDataSetSize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_data_set_size_in_bytes")]
		public int? TotalDataSetSizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class StoredScript
	{
		[JsonPropertyName("lang")]
		public Union<Nest.ScriptLanguage, string>? Lang { get; set; }

		[JsonPropertyName("source")]
		public string Source { get; set; }
	}

	public partial class Transform
	{
	}

	public partial class TransformContainer
	{
		[JsonPropertyName("chain")]
		public Nest.ChainTransform? Chain { get; set; }

		[JsonPropertyName("script")]
		public Nest.ScriptTransform? Script { get; set; }

		[JsonPropertyName("search")]
		public Nest.SearchTransform? Search { get; set; }
	}

	public partial class TranslogStats
	{
		[JsonPropertyName("earliest_last_modified_age")]
		public long EarliestLastModifiedAge
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("operations")]
		public long Operations
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("size")]
		public string? Size
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("size_in_bytes")]
		public long SizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("uncommitted_operations")]
		public int UncommittedOperations
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("uncommitted_size")]
		public string? UncommittedSize
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("uncommitted_size_in_bytes")]
		public long UncommittedSizeInBytes
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}

	public partial class WarmerStats
	{
		[JsonPropertyName("current")]
		public long Current
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total")]
		public long Total
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time")]
		public string? TotalTime
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}

		[JsonPropertyName("total_time_in_millis")]
		public long TotalTimeInMillis
		{
			get;
#if NET5_0
			init;
#else
			internal set;
#endif
		}
	}
}