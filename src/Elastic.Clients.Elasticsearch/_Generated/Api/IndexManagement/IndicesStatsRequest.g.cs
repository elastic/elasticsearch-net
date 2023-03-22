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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed class IndicesStatsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Comma-separated list or wildcard expressions of fields to include in fielddata and suggest statistics.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? CompletionFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("completion_fields"); set => Q("completion_fields", value); }

	/// <summary>
	/// <para>Type of index that wildcard patterns can match. If the request can target data streams, this argument<br/>determines whether wildcard expressions match hidden data streams. Supports comma-separated values,<br/>such as `open,hidden`.</para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>Comma-separated list or wildcard expressions of fields to include in fielddata statistics.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? FielddataFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fielddata_fields"); set => Q("fielddata_fields", value); }

	/// <summary>
	/// <para>Comma-separated list or wildcard expressions of fields to include in the statistics.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fields? Fields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fields"); set => Q("fields", value); }

	/// <summary>
	/// <para>If true, statistics are not collected from closed indices.</para>
	/// </summary>
	public bool? ForbidClosedIndices { get => Q<bool?>("forbid_closed_indices"); set => Q("forbid_closed_indices", value); }

	/// <summary>
	/// <para>Comma-separated list of search groups to include in the search statistics.</para>
	/// </summary>
	public ICollection<string>? Groups { get => Q<ICollection<string>?>("groups"); set => Q("groups", value); }

	/// <summary>
	/// <para>If true, the call reports the aggregated disk usage of each one of the Lucene index files (only applies if segment stats are requested).</para>
	/// </summary>
	public bool? IncludeSegmentFileSizes { get => Q<bool?>("include_segment_file_sizes"); set => Q("include_segment_file_sizes", value); }

	/// <summary>
	/// <para>If true, the response includes information from segments that are not loaded into memory.</para>
	/// </summary>
	public bool? IncludeUnloadedSegments { get => Q<bool?>("include_unloaded_segments"); set => Q("include_unloaded_segments", value); }

	/// <summary>
	/// <para>Indicates whether statistics are aggregated at the cluster, index, or shard level.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Level? Level { get => Q<Elastic.Clients.Elasticsearch.Level?>("level"); set => Q("level", value); }
}

/// <summary>
/// <para>Provides statistics on operations happening in an index.</para>
/// </summary>
public sealed partial class IndicesStatsRequest : PlainRequest<IndicesStatsRequestParameters>
{
	public IndicesStatsRequest()
	{
	}

	public IndicesStatsRequest(Elastic.Clients.Elasticsearch.Metrics? metric) : base(r => r.Optional("metric", metric))
	{
	}

	public IndicesStatsRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public IndicesStatsRequest(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Metrics? metric) : base(r => r.Optional("index", indices).Optional("metric", metric))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	/// <summary>
	/// <para>Comma-separated list or wildcard expressions of fields to include in fielddata and suggest statistics.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Fields? CompletionFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("completion_fields"); set => Q("completion_fields", value); }

	/// <summary>
	/// <para>Type of index that wildcard patterns can match. If the request can target data streams, this argument<br/>determines whether wildcard expressions match hidden data streams. Supports comma-separated values,<br/>such as `open,hidden`.</para>
	/// </summary>
	[JsonIgnore]
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>Comma-separated list or wildcard expressions of fields to include in fielddata statistics.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Fields? FielddataFields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fielddata_fields"); set => Q("fielddata_fields", value); }

	/// <summary>
	/// <para>Comma-separated list or wildcard expressions of fields to include in the statistics.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Fields? Fields { get => Q<Elastic.Clients.Elasticsearch.Fields?>("fields"); set => Q("fields", value); }

	/// <summary>
	/// <para>If true, statistics are not collected from closed indices.</para>
	/// </summary>
	[JsonIgnore]
	public bool? ForbidClosedIndices { get => Q<bool?>("forbid_closed_indices"); set => Q("forbid_closed_indices", value); }

	/// <summary>
	/// <para>Comma-separated list of search groups to include in the search statistics.</para>
	/// </summary>
	[JsonIgnore]
	public ICollection<string>? Groups { get => Q<ICollection<string>?>("groups"); set => Q("groups", value); }

	/// <summary>
	/// <para>If true, the call reports the aggregated disk usage of each one of the Lucene index files (only applies if segment stats are requested).</para>
	/// </summary>
	[JsonIgnore]
	public bool? IncludeSegmentFileSizes { get => Q<bool?>("include_segment_file_sizes"); set => Q("include_segment_file_sizes", value); }

	/// <summary>
	/// <para>If true, the response includes information from segments that are not loaded into memory.</para>
	/// </summary>
	[JsonIgnore]
	public bool? IncludeUnloadedSegments { get => Q<bool?>("include_unloaded_segments"); set => Q("include_unloaded_segments", value); }

	/// <summary>
	/// <para>Indicates whether statistics are aggregated at the cluster, index, or shard level.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Level? Level { get => Q<Elastic.Clients.Elasticsearch.Level?>("level"); set => Q("level", value); }
}

/// <summary>
/// <para>Provides statistics on operations happening in an index.</para>
/// </summary>
public sealed partial class IndicesStatsRequestDescriptor<TDocument> : RequestDescriptor<IndicesStatsRequestDescriptor<TDocument>, IndicesStatsRequestParameters>
{
	internal IndicesStatsRequestDescriptor(Action<IndicesStatsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public IndicesStatsRequestDescriptor()
	{
	}

	public IndicesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public IndicesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Metrics? metric) : base(r => r.Optional("index", indices).Optional("metric", metric))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	public IndicesStatsRequestDescriptor<TDocument> CompletionFields(Elastic.Clients.Elasticsearch.Fields? completionFields) => Qs("completion_fields", completionFields);
	public IndicesStatsRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public IndicesStatsRequestDescriptor<TDocument> FielddataFields(Elastic.Clients.Elasticsearch.Fields? fielddataFields) => Qs("fielddata_fields", fielddataFields);
	public IndicesStatsRequestDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields? fields) => Qs("fields", fields);
	public IndicesStatsRequestDescriptor<TDocument> ForbidClosedIndices(bool? forbidClosedIndices = true) => Qs("forbid_closed_indices", forbidClosedIndices);
	public IndicesStatsRequestDescriptor<TDocument> Groups(ICollection<string>? groups) => Qs("groups", groups);
	public IndicesStatsRequestDescriptor<TDocument> IncludeSegmentFileSizes(bool? includeSegmentFileSizes = true) => Qs("include_segment_file_sizes", includeSegmentFileSizes);
	public IndicesStatsRequestDescriptor<TDocument> IncludeUnloadedSegments(bool? includeUnloadedSegments = true) => Qs("include_unloaded_segments", includeUnloadedSegments);
	public IndicesStatsRequestDescriptor<TDocument> Level(Elastic.Clients.Elasticsearch.Level? level) => Qs("level", level);

	public IndicesStatsRequestDescriptor<TDocument> Metric(Elastic.Clients.Elasticsearch.Metrics? metric)
	{
		RouteValues.Optional("metric", metric);
		return Self;
	}

	public IndicesStatsRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>Provides statistics on operations happening in an index.</para>
/// </summary>
public sealed partial class IndicesStatsRequestDescriptor : RequestDescriptor<IndicesStatsRequestDescriptor, IndicesStatsRequestParameters>
{
	internal IndicesStatsRequestDescriptor(Action<IndicesStatsRequestDescriptor> configure) => configure.Invoke(this);

	public IndicesStatsRequestDescriptor()
	{
	}

	public IndicesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public IndicesStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Metrics? metric) : base(r => r.Optional("index", indices).Optional("metric", metric))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	public IndicesStatsRequestDescriptor CompletionFields(Elastic.Clients.Elasticsearch.Fields? completionFields) => Qs("completion_fields", completionFields);
	public IndicesStatsRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public IndicesStatsRequestDescriptor FielddataFields(Elastic.Clients.Elasticsearch.Fields? fielddataFields) => Qs("fielddata_fields", fielddataFields);
	public IndicesStatsRequestDescriptor Fields(Elastic.Clients.Elasticsearch.Fields? fields) => Qs("fields", fields);
	public IndicesStatsRequestDescriptor ForbidClosedIndices(bool? forbidClosedIndices = true) => Qs("forbid_closed_indices", forbidClosedIndices);
	public IndicesStatsRequestDescriptor Groups(ICollection<string>? groups) => Qs("groups", groups);
	public IndicesStatsRequestDescriptor IncludeSegmentFileSizes(bool? includeSegmentFileSizes = true) => Qs("include_segment_file_sizes", includeSegmentFileSizes);
	public IndicesStatsRequestDescriptor IncludeUnloadedSegments(bool? includeUnloadedSegments = true) => Qs("include_unloaded_segments", includeUnloadedSegments);
	public IndicesStatsRequestDescriptor Level(Elastic.Clients.Elasticsearch.Level? level) => Qs("level", level);

	public IndicesStatsRequestDescriptor Metric(Elastic.Clients.Elasticsearch.Metrics? metric)
	{
		RouteValues.Optional("metric", metric);
		return Self;
	}

	public IndicesStatsRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}