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

public sealed class GetDataLifecycleRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Whether wildcard expressions should get expanded to open or closed indices (default: open)</para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>Return all relevant default configurations for the data stream (default: false)</para>
	/// </summary>
	public bool? IncludeDefaults { get => Q<bool?>("include_defaults"); set => Q("include_defaults", value); }
}

/// <summary>
/// EXPERIMENTAL! May change in ways that are not backwards compatible or be removed entirely.
/// <para>Retrieves the data lifecycle configuration of one or more data streams.</para>
/// </summary>
public sealed partial class GetDataLifecycleRequest : PlainRequest<GetDataLifecycleRequestParameters>
{
	public GetDataLifecycleRequest(Elastic.Clients.Elasticsearch.DataStreamNames name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementGetDataLifecycle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	/// <summary>
	/// <para>Whether wildcard expressions should get expanded to open or closed indices (default: open)</para>
	/// </summary>
	[JsonIgnore]
	public ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>Return all relevant default configurations for the data stream (default: false)</para>
	/// </summary>
	[JsonIgnore]
	public bool? IncludeDefaults { get => Q<bool?>("include_defaults"); set => Q("include_defaults", value); }
}

/// <summary>
/// EXPERIMENTAL! May change in ways that are not backwards compatible or be removed entirely.
/// <para>Retrieves the data lifecycle configuration of one or more data streams.</para>
/// </summary>
public sealed partial class GetDataLifecycleRequestDescriptor : RequestDescriptor<GetDataLifecycleRequestDescriptor, GetDataLifecycleRequestParameters>
{
	internal GetDataLifecycleRequestDescriptor(Action<GetDataLifecycleRequestDescriptor> configure) => configure.Invoke(this);

	public GetDataLifecycleRequestDescriptor(Elastic.Clients.Elasticsearch.DataStreamNames name) : base(r => r.Required("name", name))
	{
	}

	internal GetDataLifecycleRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementGetDataLifecycle;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	public GetDataLifecycleRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public GetDataLifecycleRequestDescriptor IncludeDefaults(bool? includeDefaults = true) => Qs("include_defaults", includeDefaults);

	public GetDataLifecycleRequestDescriptor Name(Elastic.Clients.Elasticsearch.DataStreamNames name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}