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
/// Resolve the cluster.
/// Resolve the specified index expressions to return information about each cluster, including the local cluster, if included.
/// Multiple patterns and remote clusters are supported.
/// </para>
/// <para>
/// This endpoint is useful before doing a cross-cluster search in order to determine which remote clusters should be included in a search.
/// </para>
/// <para>
/// You use the same index expression with this endpoint as you would for cross-cluster search.
/// Index and cluster exclusions are also supported with this endpoint.
/// </para>
/// <para>
/// For each cluster in the index expression, information is returned about:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Whether the querying ("local") cluster is currently connected to each remote cluster in the index expression scope.
/// </para>
/// </item>
/// <item>
/// <para>
/// Whether each remote cluster is configured with <c>skip_unavailable</c> as <c>true</c> or <c>false</c>.
/// </para>
/// </item>
/// <item>
/// <para>
/// Whether there are any indices, aliases, or data streams on that cluster that match the index expression.
/// </para>
/// </item>
/// <item>
/// <para>
/// Whether the search is likely to have errors returned when you do the cross-cluster search (including any authorization errors if you do not have permission to query the index).
/// </para>
/// </item>
/// <item>
/// <para>
/// Cluster version information, including the Elasticsearch server version.
/// </para>
/// </item>
/// </list>
/// <para>
/// For example, <c>GET /_resolve/cluster/my-index-*,cluster*:my-index-*</c> returns information about the local cluster and all remotely configured clusters that start with the alias <c>cluster*</c>.
/// Each cluster returns information about whether it has any indices, aliases or data streams that match <c>my-index-*</c>.
/// </para>
/// <para>
/// <strong>Advantages of using this endpoint before a cross-cluster search</strong>
/// </para>
/// <para>
/// You may want to exclude a cluster or index from a search when:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// A remote cluster is not currently connected and is configured with <c>skip_unavailable=false</c>. Running a cross-cluster search under those conditions will cause the entire search to fail.
/// </para>
/// </item>
/// <item>
/// <para>
/// A cluster has no matching indices, aliases or data streams for the index expression (or your user does not have permissions to search them). For example, suppose your index expression is <c>logs*,remote1:logs*</c> and the remote1 cluster has no indices, aliases or data streams that match <c>logs*</c>. In that case, that cluster will return no results from that cluster if you include it in a cross-cluster search.
/// </para>
/// </item>
/// <item>
/// <para>
/// The index expression (combined with any query parameters you specify) will likely cause an exception to be thrown when you do the search. In these cases, the "error" field in the <c>_resolve/cluster</c> response will be present. (This is also where security/permission errors will be shown.)
/// </para>
/// </item>
/// <item>
/// <para>
/// A remote cluster is an older version that does not support the feature you want to use in your search.
/// </para>
/// </item>
/// </list>
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
/// Resolve the cluster.
/// Resolve the specified index expressions to return information about each cluster, including the local cluster, if included.
/// Multiple patterns and remote clusters are supported.
/// </para>
/// <para>
/// This endpoint is useful before doing a cross-cluster search in order to determine which remote clusters should be included in a search.
/// </para>
/// <para>
/// You use the same index expression with this endpoint as you would for cross-cluster search.
/// Index and cluster exclusions are also supported with this endpoint.
/// </para>
/// <para>
/// For each cluster in the index expression, information is returned about:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Whether the querying ("local") cluster is currently connected to each remote cluster in the index expression scope.
/// </para>
/// </item>
/// <item>
/// <para>
/// Whether each remote cluster is configured with <c>skip_unavailable</c> as <c>true</c> or <c>false</c>.
/// </para>
/// </item>
/// <item>
/// <para>
/// Whether there are any indices, aliases, or data streams on that cluster that match the index expression.
/// </para>
/// </item>
/// <item>
/// <para>
/// Whether the search is likely to have errors returned when you do the cross-cluster search (including any authorization errors if you do not have permission to query the index).
/// </para>
/// </item>
/// <item>
/// <para>
/// Cluster version information, including the Elasticsearch server version.
/// </para>
/// </item>
/// </list>
/// <para>
/// For example, <c>GET /_resolve/cluster/my-index-*,cluster*:my-index-*</c> returns information about the local cluster and all remotely configured clusters that start with the alias <c>cluster*</c>.
/// Each cluster returns information about whether it has any indices, aliases or data streams that match <c>my-index-*</c>.
/// </para>
/// <para>
/// <strong>Advantages of using this endpoint before a cross-cluster search</strong>
/// </para>
/// <para>
/// You may want to exclude a cluster or index from a search when:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// A remote cluster is not currently connected and is configured with <c>skip_unavailable=false</c>. Running a cross-cluster search under those conditions will cause the entire search to fail.
/// </para>
/// </item>
/// <item>
/// <para>
/// A cluster has no matching indices, aliases or data streams for the index expression (or your user does not have permissions to search them). For example, suppose your index expression is <c>logs*,remote1:logs*</c> and the remote1 cluster has no indices, aliases or data streams that match <c>logs*</c>. In that case, that cluster will return no results from that cluster if you include it in a cross-cluster search.
/// </para>
/// </item>
/// <item>
/// <para>
/// The index expression (combined with any query parameters you specify) will likely cause an exception to be thrown when you do the search. In these cases, the "error" field in the <c>_resolve/cluster</c> response will be present. (This is also where security/permission errors will be shown.)
/// </para>
/// </item>
/// <item>
/// <para>
/// A remote cluster is an older version that does not support the feature you want to use in your search.
/// </para>
/// </item>
/// </list>
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