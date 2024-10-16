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

public sealed partial class ResolveClusterRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If false, the request returns an error if any wildcard expression, index alias, or _all value targets only missing
	/// or closed indices. This behavior applies even if the request targets other open indices. For example, a request
	/// targeting foo*,bar* returns an error if an index starts with foo but no index starts with bar.
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
	/// If true, concrete, expanded or aliased indices are ignored when frozen. Defaults to false.
	/// </para>
	/// </summary>
	public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

	/// <summary>
	/// <para>
	/// If false, the request returns an error if it targets a missing or closed index. Defaults to false.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }
}

/// <summary>
/// <para>
/// Resolves the specified index expressions to return information about each cluster, including
/// the local cluster, if included.
/// Multiple patterns and remote clusters are supported.
/// </para>
/// </summary>
public sealed partial class ResolveClusterRequest : PlainRequest<ResolveClusterRequestParameters>
{
	public ResolveClusterRequest(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementResolveCluster;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.resolve_cluster";

	/// <summary>
	/// <para>
	/// If false, the request returns an error if any wildcard expression, index alias, or _all value targets only missing
	/// or closed indices. This behavior applies even if the request targets other open indices. For example, a request
	/// targeting foo*,bar* returns an error if an index starts with foo but no index starts with bar.
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
	/// If true, concrete, expanded or aliased indices are ignored when frozen. Defaults to false.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreThrottled { get => Q<bool?>("ignore_throttled"); set => Q("ignore_throttled", value); }

	/// <summary>
	/// <para>
	/// If false, the request returns an error if it targets a missing or closed index. Defaults to false.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }
}

/// <summary>
/// <para>
/// Resolves the specified index expressions to return information about each cluster, including
/// the local cluster, if included.
/// Multiple patterns and remote clusters are supported.
/// </para>
/// </summary>
public sealed partial class ResolveClusterRequestDescriptor : RequestDescriptor<ResolveClusterRequestDescriptor, ResolveClusterRequestParameters>
{
	internal ResolveClusterRequestDescriptor(Action<ResolveClusterRequestDescriptor> configure) => configure.Invoke(this);

	public ResolveClusterRequestDescriptor(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementResolveCluster;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.resolve_cluster";

	public ResolveClusterRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public ResolveClusterRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public ResolveClusterRequestDescriptor IgnoreThrottled(bool? ignoreThrottled = true) => Qs("ignore_throttled", ignoreThrottled);
	public ResolveClusterRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);

	public ResolveClusterRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}