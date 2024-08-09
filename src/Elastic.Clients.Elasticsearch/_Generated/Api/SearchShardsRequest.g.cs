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

namespace Elastic.Clients.Elasticsearch;

public sealed partial class SearchShardsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// For example, a request targeting <c>foo*,bar*</c> returns an error if an index starts with <c>foo</c> but no index starts with <c>bar</c>.
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

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request retrieves information from the local node only.
	/// </para>
	/// </summary>
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	/// <summary>
	/// <para>
	/// Specifies the node or shard the operation should be performed on.
	/// Random by default.
	/// </para>
	/// </summary>
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }
}

/// <summary>
/// <para>
/// Returns information about the indices and shards that a search request would be executed against.
/// </para>
/// </summary>
public sealed partial class SearchShardsRequest : PlainRequest<SearchShardsRequestParameters>
{
	public SearchShardsRequest()
	{
	}

	public SearchShardsRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceSearchShards;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "search_shards";

	/// <summary>
	/// <para>
	/// If <c>false</c>, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only missing or closed indices.
	/// This behavior applies even if the request targets other open indices.
	/// For example, a request targeting <c>foo*,bar*</c> returns an error if an index starts with <c>foo</c> but no index starts with <c>bar</c>.
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

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request retrieves information from the local node only.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	/// <summary>
	/// <para>
	/// Specifies the node or shard the operation should be performed on.
	/// Random by default.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public string? Preference { get => Q<string?>("preference"); set => Q("preference", value); }

	/// <summary>
	/// <para>
	/// Custom value used to route operations to a specific shard.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Routing? Routing { get => Q<Elastic.Clients.Elasticsearch.Routing?>("routing"); set => Q("routing", value); }
}

/// <summary>
/// <para>
/// Returns information about the indices and shards that a search request would be executed against.
/// </para>
/// </summary>
public sealed partial class SearchShardsRequestDescriptor<TDocument> : RequestDescriptor<SearchShardsRequestDescriptor<TDocument>, SearchShardsRequestParameters>
{
	internal SearchShardsRequestDescriptor(Action<SearchShardsRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SearchShardsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public SearchShardsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceSearchShards;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "search_shards";

	public SearchShardsRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public SearchShardsRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public SearchShardsRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public SearchShardsRequestDescriptor<TDocument> Local(bool? local = true) => Qs("local", local);
	public SearchShardsRequestDescriptor<TDocument> Preference(string? preference) => Qs("preference", preference);
	public SearchShardsRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);

	public SearchShardsRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
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
/// Returns information about the indices and shards that a search request would be executed against.
/// </para>
/// </summary>
public sealed partial class SearchShardsRequestDescriptor : RequestDescriptor<SearchShardsRequestDescriptor, SearchShardsRequestParameters>
{
	internal SearchShardsRequestDescriptor(Action<SearchShardsRequestDescriptor> configure) => configure.Invoke(this);

	public SearchShardsRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}

	public SearchShardsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceSearchShards;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => false;

	internal override string OperationName => "search_shards";

	public SearchShardsRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public SearchShardsRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public SearchShardsRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public SearchShardsRequestDescriptor Local(bool? local = true) => Qs("local", local);
	public SearchShardsRequestDescriptor Preference(string? preference) => Qs("preference", preference);
	public SearchShardsRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing) => Qs("routing", routing);

	public SearchShardsRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}