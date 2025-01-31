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
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class SegmentsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }
}

/// <summary>
/// <para>
/// Get index segments.
/// Get low-level information about the Lucene segments in index shards.
/// For data streams, the API returns information about the stream's backing indices.
/// </para>
/// </summary>
public sealed partial class SegmentsRequest : PlainRequest<SegmentsRequestParameters>
{
	[JsonConstructor]
	public SegmentsRequest()
	{
	}

	public SegmentsRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementSegments;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.segments";

	/// <summary>
	/// <para>
	/// Comma-separated list of data streams, indices, and aliases used to limit the request.
	/// Supports wildcards (<c>*</c>).
	/// To target all data streams and indices, omit this parameter or use <c>*</c> or <c>_all</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match.
	/// If the request can target data streams, this argument determines whether wildcard expressions match hidden data streams.
	/// Supports comma-separated values, such as <c>open,hidden</c>.
	/// Valid values are: <c>all</c>, <c>open</c>, <c>closed</c>, <c>hidden</c>, <c>none</c>.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if it targets a missing or closed index.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }
}

/// <summary>
/// <para>
/// Get index segments.
/// Get low-level information about the Lucene segments in index shards.
/// For data streams, the API returns information about the stream's backing indices.
/// </para>
/// </summary>
public sealed partial class SegmentsRequestDescriptor<TDocument> : RequestDescriptor<SegmentsRequestDescriptor<TDocument>, SegmentsRequestParameters>
{
	internal SegmentsRequestDescriptor(Action<SegmentsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SegmentsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public SegmentsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementSegments;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.segments";

	public SegmentsRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public SegmentsRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public SegmentsRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);

	public SegmentsRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Get index segments.
/// Get low-level information about the Lucene segments in index shards.
/// For data streams, the API returns information about the stream's backing indices.
/// </para>
/// </summary>
public sealed partial class SegmentsRequestDescriptor : RequestDescriptor<SegmentsRequestDescriptor, SegmentsRequestParameters>
{
	internal SegmentsRequestDescriptor(Action<SegmentsRequestDescriptor> configure) => configure.Invoke(this);

	public SegmentsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public SegmentsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementSegments;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.segments";

	public SegmentsRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public SegmentsRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public SegmentsRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);

	public SegmentsRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}