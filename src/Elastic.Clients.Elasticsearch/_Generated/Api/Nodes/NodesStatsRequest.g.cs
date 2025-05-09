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

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Nodes;

public sealed partial class NodesStatsRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata and suggest statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? CompletionFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("completion_fields"); set => Q("completion_fields", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? FielddataFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fielddata_fields"); set => Q("fielddata_fields", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in the statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Fields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fields"); set => Q("fields", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list of search groups to include in the search statistics.
	/// </para>
	/// </summary>
	public bool? Groups { get => Q<bool?>("groups"); set => Q("groups", value); }

	/// <summary>
	/// <para>
	/// If true, the call reports the aggregated disk usage of each one of the Lucene index files (only applies if segment stats are requested).
	/// </para>
	/// </summary>
	public bool? IncludeSegmentFileSizes { get => Q<bool?>("include_segment_file_sizes"); set => Q("include_segment_file_sizes", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes information from segments that are not loaded into memory.
	/// </para>
	/// </summary>
	public bool? IncludeUnloadedSegments { get => Q<bool?>("include_unloaded_segments"); set => Q("include_unloaded_segments", value); }

	/// <summary>
	/// <para>
	/// Indicates whether statistics are aggregated at the cluster, index, or shard level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Level? Level { get => Q<Elastic.Clients.Elasticsearch.Level?>("level"); set => Q("level", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of document types for the indexing index metric.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Types { get => Q<System.Collections.Generic.ICollection<string>?>("types"); set => Q("types", value); }
}

internal sealed partial class NodesStatsRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest>
{
	public override Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get node statistics.
/// Get statistics for nodes in a cluster.
/// By default, all stats are returned. You can limit the returned information by using metrics.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestConverter))]
public sealed partial class NodesStatsRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestParameters>
{
	public NodesStatsRequest(Elastic.Clients.Elasticsearch.NodeIds? nodeId) : base(r => r.Optional("node_id", nodeId))
	{
	}

	public NodesStatsRequest(Elastic.Clients.Elasticsearch.Metrics? metric) : base(r => r.Optional("metric", metric))
	{
	}

	public NodesStatsRequest(Elastic.Clients.Elasticsearch.NodeIds? nodeId, Elastic.Clients.Elasticsearch.Metrics? metric) : base(r => r.Optional("node_id", nodeId).Optional("metric", metric))
	{
	}

	public NodesStatsRequest(Elastic.Clients.Elasticsearch.Metrics? metric, Elastic.Clients.Elasticsearch.Metrics? indexMetric) : base(r => r.Optional("metric", metric).Optional("index_metric", indexMetric))
	{
	}

	public NodesStatsRequest(Elastic.Clients.Elasticsearch.NodeIds? nodeId, Elastic.Clients.Elasticsearch.Metrics? metric, Elastic.Clients.Elasticsearch.Metrics? indexMetric) : base(r => r.Optional("node_id", nodeId).Optional("metric", metric).Optional("index_metric", indexMetric))
	{
	}
#if NET7_0_OR_GREATER
	public NodesStatsRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public NodesStatsRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NodesStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.NodesStats;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "nodes.stats";

	/// <summary>
	/// <para>
	/// Limit the information returned for indices metric to the specific index metrics. It can be used only if indices (or all) metric is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Metrics? IndexMetric { get => P<Elastic.Clients.Elasticsearch.Metrics?>("index_metric"); set => PO("index_metric", value); }

	/// <summary>
	/// <para>
	/// Limit the information returned to the specified metrics
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Metrics? Metric { get => P<Elastic.Clients.Elasticsearch.Metrics?>("metric"); set => PO("metric", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list of node IDs or names used to limit returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.NodeIds? NodeId { get => P<Elastic.Clients.Elasticsearch.NodeIds?>("node_id"); set => PO("node_id", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata and suggest statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? CompletionFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("completion_fields"); set => Q("completion_fields", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? FielddataFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fielddata_fields"); set => Q("fielddata_fields", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in the statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Fields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fields"); set => Q("fields", value); }

	/// <summary>
	/// <para>
	/// Comma-separated list of search groups to include in the search statistics.
	/// </para>
	/// </summary>
	public bool? Groups { get => Q<bool?>("groups"); set => Q("groups", value); }

	/// <summary>
	/// <para>
	/// If true, the call reports the aggregated disk usage of each one of the Lucene index files (only applies if segment stats are requested).
	/// </para>
	/// </summary>
	public bool? IncludeSegmentFileSizes { get => Q<bool?>("include_segment_file_sizes"); set => Q("include_segment_file_sizes", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes information from segments that are not loaded into memory.
	/// </para>
	/// </summary>
	public bool? IncludeUnloadedSegments { get => Q<bool?>("include_unloaded_segments"); set => Q("include_unloaded_segments", value); }

	/// <summary>
	/// <para>
	/// Indicates whether statistics are aggregated at the cluster, index, or shard level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Level? Level { get => Q<Elastic.Clients.Elasticsearch.Level?>("level"); set => Q("level", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// A comma-separated list of document types for the indexing index metric.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<string>? Types { get => Q<System.Collections.Generic.ICollection<string>?>("types"); set => Q("types", value); }
}

/// <summary>
/// <para>
/// Get node statistics.
/// Get statistics for nodes in a cluster.
/// By default, all stats are returned. You can limit the returned information by using metrics.
/// </para>
/// </summary>
public readonly partial struct NodesStatsRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest instance)
	{
		Instance = instance;
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(nodeId);
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Metrics? metric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(metric);
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId, Elastic.Clients.Elasticsearch.Metrics? metric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(nodeId, metric);
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Metrics? metric, Elastic.Clients.Elasticsearch.Metrics? indexMetric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(metric, indexMetric);
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId, Elastic.Clients.Elasticsearch.Metrics? metric, Elastic.Clients.Elasticsearch.Metrics? indexMetric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(nodeId, metric, indexMetric);
	}

	public NodesStatsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest instance) => new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Limit the information returned for indices metric to the specific index metrics. It can be used only if indices (or all) metric is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor IndexMetric(Elastic.Clients.Elasticsearch.Metrics? value)
	{
		Instance.IndexMetric = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Limit the information returned to the specified metrics
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Metric(Elastic.Clients.Elasticsearch.Metrics? value)
	{
		Instance.Metric = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list of node IDs or names used to limit returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor NodeId(Elastic.Clients.Elasticsearch.NodeIds? value)
	{
		Instance.NodeId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata and suggest statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor CompletionFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CompletionFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata and suggest statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor CompletionFields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.CompletionFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor FielddataFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.FielddataFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor FielddataFields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.FielddataFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in the statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Fields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in the statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Fields<T>(params System.Linq.Expressions.Expression<System.Func<T, object?>>[] value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list of search groups to include in the search statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Groups(bool? value = true)
	{
		Instance.Groups = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the call reports the aggregated disk usage of each one of the Lucene index files (only applies if segment stats are requested).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor IncludeSegmentFileSizes(bool? value = true)
	{
		Instance.IncludeSegmentFileSizes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes information from segments that are not loaded into memory.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor IncludeUnloadedSegments(bool? value = true)
	{
		Instance.IncludeUnloadedSegments = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether statistics are aggregated at the cluster, index, or shard level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Level(Elastic.Clients.Elasticsearch.Level? value)
	{
		Instance.Level = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of document types for the indexing index metric.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Types(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Types = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of document types for the indexing index metric.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Types(params string[] values)
	{
		Instance.Types = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest Build(System.Action<Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor(new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Get node statistics.
/// Get statistics for nodes in a cluster.
/// By default, all stats are returned. You can limit the returned information by using metrics.
/// </para>
/// </summary>
public readonly partial struct NodesStatsRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest instance)
	{
		Instance = instance;
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(nodeId);
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Metrics? metric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(metric);
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId, Elastic.Clients.Elasticsearch.Metrics? metric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(nodeId, metric);
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Metrics? metric, Elastic.Clients.Elasticsearch.Metrics? indexMetric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(metric, indexMetric);
	}

	public NodesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.NodeIds? nodeId, Elastic.Clients.Elasticsearch.Metrics? metric, Elastic.Clients.Elasticsearch.Metrics? indexMetric)
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(nodeId, metric, indexMetric);
	}

	public NodesStatsRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest instance) => new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Limit the information returned for indices metric to the specific index metrics. It can be used only if indices (or all) metric is specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> IndexMetric(Elastic.Clients.Elasticsearch.Metrics? value)
	{
		Instance.IndexMetric = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Limit the information returned to the specified metrics
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Metric(Elastic.Clients.Elasticsearch.Metrics? value)
	{
		Instance.Metric = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list of node IDs or names used to limit returned information.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> NodeId(Elastic.Clients.Elasticsearch.NodeIds? value)
	{
		Instance.NodeId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata and suggest statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> CompletionFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.CompletionFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata and suggest statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> CompletionFields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.CompletionFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> FielddataFields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.FielddataFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in fielddata statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> FielddataFields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.FielddataFields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in the statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields? value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list or wildcard expressions of fields to include in the statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Fields(params System.Linq.Expressions.Expression<System.Func<TDocument, object?>>[] value)
	{
		Instance.Fields = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Comma-separated list of search groups to include in the search statistics.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Groups(bool? value = true)
	{
		Instance.Groups = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the call reports the aggregated disk usage of each one of the Lucene index files (only applies if segment stats are requested).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> IncludeSegmentFileSizes(bool? value = true)
	{
		Instance.IncludeSegmentFileSizes = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the response includes information from segments that are not loaded into memory.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> IncludeUnloadedSegments(bool? value = true)
	{
		Instance.IncludeUnloadedSegments = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates whether statistics are aggregated at the cluster, index, or shard level.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Level(Elastic.Clients.Elasticsearch.Level? value)
	{
		Instance.Level = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response. If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of document types for the indexing index metric.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Types(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Types = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A comma-separated list of document types for the indexing index metric.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Types(params string[] values)
	{
		Instance.Types = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest Build(System.Action<Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Nodes.NodesStatsRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}