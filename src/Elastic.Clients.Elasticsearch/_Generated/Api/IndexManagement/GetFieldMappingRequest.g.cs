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

public sealed partial class GetFieldMappingRequestParameters : RequestParameters
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

	/// <summary>
	/// <para>
	/// If <c>true</c>, return all default settings in the response.
	/// </para>
	/// </summary>
	public bool? IncludeDefaults { get => Q<bool?>("include_defaults"); set => Q("include_defaults", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request retrieves information from the local node only.
	/// </para>
	/// </summary>
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }
}

/// <summary>
/// <para>
/// Get mapping definitions.
/// Retrieves mapping definitions for one or more fields.
/// For data streams, the API retrieves field mappings for the stream’s backing indices.
/// </para>
/// <para>
/// This API is useful if you don't need a complete mapping or if an index mapping contains a large number of fields.
/// </para>
/// </summary>
public sealed partial class GetFieldMappingRequest : PlainRequest<GetFieldMappingRequestParameters>
{
	public GetFieldMappingRequest(Elastic.Clients.Elasticsearch.Fields fields) : base(r => r.Required("fields", fields))
	{
	}

	public GetFieldMappingRequest(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Fields fields) : base(r => r.Optional("index", indices).Required("fields", fields))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementGetFieldMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.get_field_mapping";

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

	/// <summary>
	/// <para>
	/// If <c>true</c>, return all default settings in the response.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? IncludeDefaults { get => Q<bool?>("include_defaults"); set => Q("include_defaults", value); }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the request retrieves information from the local node only.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }
}

/// <summary>
/// <para>
/// Get mapping definitions.
/// Retrieves mapping definitions for one or more fields.
/// For data streams, the API retrieves field mappings for the stream’s backing indices.
/// </para>
/// <para>
/// This API is useful if you don't need a complete mapping or if an index mapping contains a large number of fields.
/// </para>
/// </summary>
public sealed partial class GetFieldMappingRequestDescriptor<TDocument> : RequestDescriptor<GetFieldMappingRequestDescriptor<TDocument>, GetFieldMappingRequestParameters>
{
	internal GetFieldMappingRequestDescriptor(Action<GetFieldMappingRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GetFieldMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Fields fields) : base(r => r.Optional("index", indices).Required("fields", fields))
	{
	}

	public GetFieldMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Fields fields) : base(r => r.Required("fields", fields))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementGetFieldMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.get_field_mapping";

	public GetFieldMappingRequestDescriptor<TDocument> AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public GetFieldMappingRequestDescriptor<TDocument> ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public GetFieldMappingRequestDescriptor<TDocument> IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public GetFieldMappingRequestDescriptor<TDocument> IncludeDefaults(bool? includeDefaults = true) => Qs("include_defaults", includeDefaults);
	public GetFieldMappingRequestDescriptor<TDocument> Local(bool? local = true) => Qs("local", local);

	public GetFieldMappingRequestDescriptor<TDocument> Fields(Elastic.Clients.Elasticsearch.Fields fields)
	{
		RouteValues.Required("fields", fields);
		return Self;
	}

	public GetFieldMappingRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? indices)
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
/// Get mapping definitions.
/// Retrieves mapping definitions for one or more fields.
/// For data streams, the API retrieves field mappings for the stream’s backing indices.
/// </para>
/// <para>
/// This API is useful if you don't need a complete mapping or if an index mapping contains a large number of fields.
/// </para>
/// </summary>
public sealed partial class GetFieldMappingRequestDescriptor : RequestDescriptor<GetFieldMappingRequestDescriptor, GetFieldMappingRequestParameters>
{
	internal GetFieldMappingRequestDescriptor(Action<GetFieldMappingRequestDescriptor> configure) => configure.Invoke(this);

	public GetFieldMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices, Elastic.Clients.Elasticsearch.Fields fields) : base(r => r.Optional("index", indices).Required("fields", fields))
	{
	}

	public GetFieldMappingRequestDescriptor(Elastic.Clients.Elasticsearch.Fields fields) : base(r => r.Required("fields", fields))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementGetFieldMapping;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.get_field_mapping";

	public GetFieldMappingRequestDescriptor AllowNoIndices(bool? allowNoIndices = true) => Qs("allow_no_indices", allowNoIndices);
	public GetFieldMappingRequestDescriptor ExpandWildcards(ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? expandWildcards) => Qs("expand_wildcards", expandWildcards);
	public GetFieldMappingRequestDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) => Qs("ignore_unavailable", ignoreUnavailable);
	public GetFieldMappingRequestDescriptor IncludeDefaults(bool? includeDefaults = true) => Qs("include_defaults", includeDefaults);
	public GetFieldMappingRequestDescriptor Local(bool? local = true) => Qs("local", local);

	public GetFieldMappingRequestDescriptor Fields(Elastic.Clients.Elasticsearch.Fields fields)
	{
		RouteValues.Required("fields", fields);
		return Self;
	}

	public GetFieldMappingRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		RouteValues.Optional("index", indices);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}