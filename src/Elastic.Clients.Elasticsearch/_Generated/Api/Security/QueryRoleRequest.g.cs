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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class QueryRoleRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Find roles with a query.
/// </para>
/// <para>
/// Get roles in a paginated manner.
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The query roles API does not retrieve roles that are defined in roles files, nor built-in ones.
/// You can optionally filter the results with a query.
/// Also, the results can be paginated and sorted.
/// </para>
/// </summary>
public sealed partial class QueryRoleRequest : PlainRequest<QueryRoleRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityQueryRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.query_role";

	/// <summary>
	/// <para>
	/// The starting document offset.
	/// It must not be negative.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("from")]
	public int? From { get; set; }

	/// <summary>
	/// <para>
	/// A query to filter which roles to return.
	/// If the query parameter is missing, it is equivalent to a <c>match_all</c> query.
	/// The query supports a subset of query types, including <c>match_all</c>, <c>bool</c>, <c>term</c>, <c>terms</c>, <c>match</c>,
	/// <c>ids</c>, <c>prefix</c>, <c>wildcard</c>, <c>exists</c>, <c>range</c>, and <c>simple_query_string</c>.
	/// You can query the following information associated with roles: <c>name</c>, <c>description</c>, <c>metadata</c>,
	/// <c>applications.application</c>, <c>applications.privileges</c>, and <c>applications.resources</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public Elastic.Clients.Elasticsearch.Security.RoleQuery? Query { get; set; }

	/// <summary>
	/// <para>
	/// The search after definition.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("search_after")]
	public ICollection<Elastic.Clients.Elasticsearch.FieldValue>? SearchAfter { get; set; }

	/// <summary>
	/// <para>
	/// The number of hits to return.
	/// It must not be negative.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// The sort definition.
	/// You can sort on <c>username</c>, <c>roles</c>, or <c>enabled</c>.
	/// In addition, sort can also be applied to the <c>_doc</c> field to sort by index order.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("sort")]
	[SingleOrManyCollectionConverter(typeof(Elastic.Clients.Elasticsearch.SortOptions))]
	public ICollection<Elastic.Clients.Elasticsearch.SortOptions>? Sort { get; set; }
}

/// <summary>
/// <para>
/// Find roles with a query.
/// </para>
/// <para>
/// Get roles in a paginated manner.
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The query roles API does not retrieve roles that are defined in roles files, nor built-in ones.
/// You can optionally filter the results with a query.
/// Also, the results can be paginated and sorted.
/// </para>
/// </summary>
public sealed partial class QueryRoleRequestDescriptor<TDocument> : RequestDescriptor<QueryRoleRequestDescriptor<TDocument>, QueryRoleRequestParameters>
{
	internal QueryRoleRequestDescriptor(Action<QueryRoleRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public QueryRoleRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityQueryRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.query_role";

	private int? FromValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RoleQuery? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.FieldValue>? SearchAfterValue { get; set; }
	private int? SizeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument> SortDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>> SortDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>>[] SortDescriptorActions { get; set; }

	/// <summary>
	/// <para>
	/// The starting document offset.
	/// It must not be negative.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor<TDocument> From(int? from)
	{
		FromValue = from;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A query to filter which roles to return.
	/// If the query parameter is missing, it is equivalent to a <c>match_all</c> query.
	/// The query supports a subset of query types, including <c>match_all</c>, <c>bool</c>, <c>term</c>, <c>terms</c>, <c>match</c>,
	/// <c>ids</c>, <c>prefix</c>, <c>wildcard</c>, <c>exists</c>, <c>range</c>, and <c>simple_query_string</c>.
	/// You can query the following information associated with roles: <c>name</c>, <c>description</c>, <c>metadata</c>,
	/// <c>applications.application</c>, <c>applications.privileges</c>, and <c>applications.resources</c>.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Security.RoleQuery? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public QueryRoleRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public QueryRoleRequestDescriptor<TDocument> Query(Action<Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The search after definition.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor<TDocument> SearchAfter(ICollection<Elastic.Clients.Elasticsearch.FieldValue>? searchAfter)
	{
		SearchAfterValue = searchAfter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of hits to return.
	/// It must not be negative.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The sort definition.
	/// You can sort on <c>username</c>, <c>roles</c>, or <c>enabled</c>.
	/// In addition, sort can also be applied to the <c>_doc</c> field to sort by index order.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor<TDocument> Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public QueryRoleRequestDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument> descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public QueryRoleRequestDescriptor<TDocument> Sort(Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public QueryRoleRequestDescriptor<TDocument> Sort(params Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FromValue.HasValue)
		{
			writer.WritePropertyName("from");
			writer.WriteNumberValue(FromValue.Value);
		}

		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor<TDocument>(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (SearchAfterValue is not null)
		{
			writer.WritePropertyName("search_after");
			JsonSerializer.Serialize(writer, SearchAfterValue, options);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (SortDescriptor is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, SortDescriptor, options);
		}
		else if (SortDescriptorAction is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>(action), options);
			}

			if (SortDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.SortOptions>(SortValue, writer, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Find roles with a query.
/// </para>
/// <para>
/// Get roles in a paginated manner.
/// The role management APIs are generally the preferred way to manage roles, rather than using file-based role management.
/// The query roles API does not retrieve roles that are defined in roles files, nor built-in ones.
/// You can optionally filter the results with a query.
/// Also, the results can be paginated and sorted.
/// </para>
/// </summary>
public sealed partial class QueryRoleRequestDescriptor : RequestDescriptor<QueryRoleRequestDescriptor, QueryRoleRequestParameters>
{
	internal QueryRoleRequestDescriptor(Action<QueryRoleRequestDescriptor> configure) => configure.Invoke(this);

	public QueryRoleRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityQueryRole;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.query_role";

	private int? FromValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RoleQuery? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor> QueryDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.FieldValue>? SearchAfterValue { get; set; }
	private int? SizeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOptionsDescriptor SortDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor> SortDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor>[] SortDescriptorActions { get; set; }

	/// <summary>
	/// <para>
	/// The starting document offset.
	/// It must not be negative.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor From(int? from)
	{
		FromValue = from;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A query to filter which roles to return.
	/// If the query parameter is missing, it is equivalent to a <c>match_all</c> query.
	/// The query supports a subset of query types, including <c>match_all</c>, <c>bool</c>, <c>term</c>, <c>terms</c>, <c>match</c>,
	/// <c>ids</c>, <c>prefix</c>, <c>wildcard</c>, <c>exists</c>, <c>range</c>, and <c>simple_query_string</c>.
	/// You can query the following information associated with roles: <c>name</c>, <c>description</c>, <c>metadata</c>,
	/// <c>applications.application</c>, <c>applications.privileges</c>, and <c>applications.resources</c>.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor Query(Elastic.Clients.Elasticsearch.Security.RoleQuery? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public QueryRoleRequestDescriptor Query(Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public QueryRoleRequestDescriptor Query(Action<Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The search after definition.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor SearchAfter(ICollection<Elastic.Clients.Elasticsearch.FieldValue>? searchAfter)
	{
		SearchAfterValue = searchAfter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of hits to return.
	/// It must not be negative.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The sort definition.
	/// You can sort on <c>username</c>, <c>roles</c>, or <c>enabled</c>.
	/// In addition, sort can also be applied to the <c>_doc</c> field to sort by index order.
	/// </para>
	/// </summary>
	public QueryRoleRequestDescriptor Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public QueryRoleRequestDescriptor Sort(Elastic.Clients.Elasticsearch.SortOptionsDescriptor descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public QueryRoleRequestDescriptor Sort(Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public QueryRoleRequestDescriptor Sort(params Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor>[] configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FromValue.HasValue)
		{
			writer.WritePropertyName("from");
			writer.WriteNumberValue(FromValue.Value);
		}

		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.RoleQueryDescriptor(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (SearchAfterValue is not null)
		{
			writer.WritePropertyName("search_after");
			JsonSerializer.Serialize(writer, SearchAfterValue, options);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (SortDescriptor is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, SortDescriptor, options);
		}
		else if (SortDescriptorAction is not null)
		{
			writer.WritePropertyName("sort");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor(SortDescriptorAction), options);
		}
		else if (SortDescriptorActions is not null)
		{
			writer.WritePropertyName("sort");
			if (SortDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in SortDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.SortOptionsDescriptor(action), options);
			}

			if (SortDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (SortValue is not null)
		{
			writer.WritePropertyName("sort");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.SortOptions>(SortValue, writer, options);
		}

		writer.WriteEndObject();
	}
}