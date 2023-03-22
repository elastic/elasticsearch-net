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

public sealed class PutAliasRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Specify timeout for connection to master</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>Explicit timestamp for the document</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>Creates or updates an alias.</para>
/// </summary>
public sealed partial class PutAliasRequest : PlainRequest<PutAliasRequestParameters>
{
	public PutAliasRequest(Elastic.Clients.Elasticsearch.Indices indices, Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("index", indices).Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementPutAlias;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	/// <summary>
	/// <para>Specify timeout for connection to master</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>Explicit timestamp for the document</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
	[JsonInclude, JsonPropertyName("filter")]
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? Filter { get; set; }
	[JsonInclude, JsonPropertyName("index_routing")]
	public Elastic.Clients.Elasticsearch.Routing? IndexRouting { get; set; }
	[JsonInclude, JsonPropertyName("is_write_index")]
	public bool? IsWriteIndex { get; set; }
	[JsonInclude, JsonPropertyName("routing")]
	public Elastic.Clients.Elasticsearch.Routing? Routing { get; set; }
	[JsonInclude, JsonPropertyName("search_routing")]
	public Elastic.Clients.Elasticsearch.Routing? SearchRouting { get; set; }
}

/// <summary>
/// <para>Creates or updates an alias.</para>
/// </summary>
public sealed partial class PutAliasRequestDescriptor<TDocument> : RequestDescriptor<PutAliasRequestDescriptor<TDocument>, PutAliasRequestParameters>
{
	internal PutAliasRequestDescriptor(Action<PutAliasRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PutAliasRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices, Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("index", indices).Required("name", name))
	{
	}

	internal PutAliasRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementPutAlias;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	public PutAliasRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutAliasRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public PutAliasRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices indices)
	{
		RouteValues.Required("index", indices);
		return Self;
	}

	public PutAliasRequestDescriptor<TDocument> Name(Elastic.Clients.Elasticsearch.Name name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.Query? FilterValue { get; set; }
	private QueryDsl.QueryDescriptor<TDocument> FilterDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor<TDocument>> FilterDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Routing? IndexRoutingValue { get; set; }
	private bool? IsWriteIndexValue { get; set; }
	private Elastic.Clients.Elasticsearch.Routing? RoutingValue { get; set; }
	private Elastic.Clients.Elasticsearch.Routing? SearchRoutingValue { get; set; }

	public PutAliasRequestDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public PutAliasRequestDescriptor<TDocument> Filter(QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public PutAliasRequestDescriptor<TDocument> Filter(Action<QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	public PutAliasRequestDescriptor<TDocument> IndexRouting(Elastic.Clients.Elasticsearch.Routing? indexRouting)
	{
		IndexRoutingValue = indexRouting;
		return Self;
	}

	public PutAliasRequestDescriptor<TDocument> IsWriteIndex(bool? isWriteIndex = true)
	{
		IsWriteIndexValue = isWriteIndex;
		return Self;
	}

	public PutAliasRequestDescriptor<TDocument> Routing(Elastic.Clients.Elasticsearch.Routing? routing)
	{
		RoutingValue = routing;
		return Self;
	}

	public PutAliasRequestDescriptor<TDocument> SearchRouting(Elastic.Clients.Elasticsearch.Routing? searchRouting)
	{
		SearchRoutingValue = searchRouting;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor<TDocument>(FilterDescriptorAction), options);
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterValue, options);
		}

		if (IndexRoutingValue is not null)
		{
			writer.WritePropertyName("index_routing");
			JsonSerializer.Serialize(writer, IndexRoutingValue, options);
		}

		if (IsWriteIndexValue.HasValue)
		{
			writer.WritePropertyName("is_write_index");
			writer.WriteBooleanValue(IsWriteIndexValue.Value);
		}

		if (RoutingValue is not null)
		{
			writer.WritePropertyName("routing");
			JsonSerializer.Serialize(writer, RoutingValue, options);
		}

		if (SearchRoutingValue is not null)
		{
			writer.WritePropertyName("search_routing");
			JsonSerializer.Serialize(writer, SearchRoutingValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>Creates or updates an alias.</para>
/// </summary>
public sealed partial class PutAliasRequestDescriptor : RequestDescriptor<PutAliasRequestDescriptor, PutAliasRequestParameters>
{
	internal PutAliasRequestDescriptor(Action<PutAliasRequestDescriptor> configure) => configure.Invoke(this);

	public PutAliasRequestDescriptor(Elastic.Clients.Elasticsearch.Indices indices, Elastic.Clients.Elasticsearch.Name name) : base(r => r.Required("index", indices).Required("name", name))
	{
	}

	internal PutAliasRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementPutAlias;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	public PutAliasRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public PutAliasRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public PutAliasRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices indices)
	{
		RouteValues.Required("index", indices);
		return Self;
	}

	public PutAliasRequestDescriptor Name(Elastic.Clients.Elasticsearch.Name name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.Query? FilterValue { get; set; }
	private QueryDsl.QueryDescriptor FilterDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor> FilterDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Routing? IndexRoutingValue { get; set; }
	private bool? IsWriteIndexValue { get; set; }
	private Elastic.Clients.Elasticsearch.Routing? RoutingValue { get; set; }
	private Elastic.Clients.Elasticsearch.Routing? SearchRoutingValue { get; set; }

	public PutAliasRequestDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.Query? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterValue = filter;
		return Self;
	}

	public PutAliasRequestDescriptor Filter(QueryDsl.QueryDescriptor descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public PutAliasRequestDescriptor Filter(Action<QueryDsl.QueryDescriptor> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	public PutAliasRequestDescriptor IndexRouting(Elastic.Clients.Elasticsearch.Routing? indexRouting)
	{
		IndexRoutingValue = indexRouting;
		return Self;
	}

	public PutAliasRequestDescriptor IsWriteIndex(bool? isWriteIndex = true)
	{
		IsWriteIndexValue = isWriteIndex;
		return Self;
	}

	public PutAliasRequestDescriptor Routing(Elastic.Clients.Elasticsearch.Routing? routing)
	{
		RoutingValue = routing;
		return Self;
	}

	public PutAliasRequestDescriptor SearchRouting(Elastic.Clients.Elasticsearch.Routing? searchRouting)
	{
		SearchRoutingValue = searchRouting;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor(FilterDescriptorAction), options);
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterValue, options);
		}

		if (IndexRoutingValue is not null)
		{
			writer.WritePropertyName("index_routing");
			JsonSerializer.Serialize(writer, IndexRoutingValue, options);
		}

		if (IsWriteIndexValue.HasValue)
		{
			writer.WritePropertyName("is_write_index");
			writer.WriteBooleanValue(IsWriteIndexValue.Value);
		}

		if (RoutingValue is not null)
		{
			writer.WritePropertyName("routing");
			JsonSerializer.Serialize(writer, RoutingValue, options);
		}

		if (SearchRoutingValue is not null)
		{
			writer.WritePropertyName("search_routing");
			JsonSerializer.Serialize(writer, SearchRoutingValue, options);
		}

		writer.WriteEndObject();
	}
}