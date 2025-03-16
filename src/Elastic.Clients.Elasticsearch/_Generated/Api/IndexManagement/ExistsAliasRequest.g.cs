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

public sealed partial class ExistsAliasRequestParameters : RequestParameters
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
	/// If <c>false</c>, requests that include a missing data stream or index in the target indices or data streams return an error.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }
}

/// <summary>
/// <para>
/// Check aliases.
/// Checks if one or more data stream or index aliases exist.
/// </para>
/// </summary>
public sealed partial class ExistsAliasRequest : PlainRequest<ExistsAliasRequestParameters>
{
	public ExistsAliasRequest(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}

	public ExistsAliasRequest(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Names name) : base(r => r.Optional("index", indices).Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementExistsAlias;

	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.exists_alias";

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
	/// If <c>false</c>, requests that include a missing data stream or index in the target indices or data streams return an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }
}

/// <summary>
/// <para>
/// Check aliases.
/// Checks if one or more data stream or index aliases exist.
/// </para>
/// </summary>
public sealed partial class ExistsAliasRequestDescriptor<TDocument> : RequestDescriptor<ExistsAliasRequestDescriptor<TDocument>, ExistsAliasRequestParameters>
{
	internal ExistsAliasRequestDescriptor(Action<ExistsAliasRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ExistsAliasRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Names name) : base(r => r.Optional("index", indices).Required("name", name))
	{
	}

	public ExistsAliasRequestDescriptor(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementExistsAlias;

	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.exists_alias";

	public ExistsAliasRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public ExistsAliasRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public ExistsAliasRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);

	public ExistsAliasRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	public ExistsAliasRequestDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Names name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

/// <summary>
/// <para>
/// Check aliases.
/// Checks if one or more data stream or index aliases exist.
/// </para>
/// </summary>
public sealed partial class ExistsAliasRequestDescriptor : RequestDescriptor<ExistsAliasRequestDescriptor, ExistsAliasRequestParameters>
{
	internal ExistsAliasRequestDescriptor(Action<ExistsAliasRequestDescriptor> configure) => configure.Invoke(this);

	public ExistsAliasRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Names name) : base(r => r.Optional("index", indices).Required("name", name))
	{
	}

	public ExistsAliasRequestDescriptor(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementExistsAlias;

	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.exists_alias";

	public ExistsAliasRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public ExistsAliasRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public ExistsAliasRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);

	public ExistsAliasRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	public ExistsAliasRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}