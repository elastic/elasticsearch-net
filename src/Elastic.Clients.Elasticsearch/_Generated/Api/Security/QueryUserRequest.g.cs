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

public sealed partial class QueryUserRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// If true will return the User Profile ID for the users in the query result, if any.
	/// </para>
	/// </summary>
	public bool? WithProfileUid { get => Q<bool?>("with_profile_uid"); set => Q("with_profile_uid", value); }
}

/// <summary>
/// <para>
/// Find users with a query.
/// </para>
/// <para>
/// Get information for users in a paginated manner.
/// You can optionally filter the results with a query.
/// </para>
/// </summary>
public sealed partial class QueryUserRequest : PlainRequest<QueryUserRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityQueryUser;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.query_user";

	/// <summary>
	/// <para>
	/// If true will return the User Profile ID for the users in the query result, if any.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? WithProfileUid { get => Q<bool?>("with_profile_uid"); set => Q("with_profile_uid", value); }

	/// <summary>
	/// <para>
	/// Starting document offset.
	/// By default, you cannot page through more than 10,000 hits using the from and size parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("from")]
	public int? From { get; set; }

	/// <summary>
	/// <para>
	/// A query to filter which users to return.
	/// If the query parameter is missing, it is equivalent to a <c>match_all</c> query.
	/// The query supports a subset of query types, including <c>match_all</c>, <c>bool</c>, <c>term</c>, <c>terms</c>, <c>match</c>,
	/// <c>ids</c>, <c>prefix</c>, <c>wildcard</c>, <c>exists</c>, <c>range</c>, and <c>simple_query_string</c>.
	/// You can query the following information associated with user: <c>username</c>, <c>roles</c>, <c>enabled</c>
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public Elastic.Clients.Elasticsearch.Security.UserQuery? Query { get; set; }

	/// <summary>
	/// <para>
	/// Search after definition
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("search_after")]
	public ICollection<Elastic.Clients.Elasticsearch.FieldValue>? SearchAfter { get; set; }

	/// <summary>
	/// <para>
	/// The number of hits to return.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// Fields eligible for sorting are: username, roles, enabled
	/// In addition, sort can also be applied to the <c>_doc</c> field to sort by index order.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("sort")]
	[SingleOrManyCollectionConverter(typeof(Elastic.Clients.Elasticsearch.SortOptions))]
	public ICollection<Elastic.Clients.Elasticsearch.SortOptions>? Sort { get; set; }
}

/// <summary>
/// <para>
/// Find users with a query.
/// </para>
/// <para>
/// Get information for users in a paginated manner.
/// You can optionally filter the results with a query.
/// </para>
/// </summary>
public sealed partial class QueryUserRequestDescriptor<TDocument> : RequestDescriptor<QueryUserRequestDescriptor<TDocument>, QueryUserRequestParameters>
{
	internal QueryUserRequestDescriptor(Action<QueryUserRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public QueryUserRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityQueryUser;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.query_user";

	public QueryUserRequestDescriptor<TDocument> WithProfileUid(bool? withProfileUid = true) => Qs("with_profile_uid", withProfileUid);

	private int? FromValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.UserQuery? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.FieldValue>? SearchAfterValue { get; set; }
	private int? SizeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument> SortDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>> SortDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>>[] SortDescriptorActions { get; set; }

	/// <summary>
	/// <para>
	/// Starting document offset.
	/// By default, you cannot page through more than 10,000 hits using the from and size parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor<TDocument> From(int? from)
	{
		FromValue = from;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A query to filter which users to return.
	/// If the query parameter is missing, it is equivalent to a <c>match_all</c> query.
	/// The query supports a subset of query types, including <c>match_all</c>, <c>bool</c>, <c>term</c>, <c>terms</c>, <c>match</c>,
	/// <c>ids</c>, <c>prefix</c>, <c>wildcard</c>, <c>exists</c>, <c>range</c>, and <c>simple_query_string</c>.
	/// You can query the following information associated with user: <c>username</c>, <c>roles</c>, <c>enabled</c>
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Security.UserQuery? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public QueryUserRequestDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public QueryUserRequestDescriptor<TDocument> Query(Action<Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Search after definition
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor<TDocument> SearchAfter(ICollection<Elastic.Clients.Elasticsearch.FieldValue>? searchAfter)
	{
		SearchAfterValue = searchAfter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of hits to return.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Fields eligible for sorting are: username, roles, enabled
	/// In addition, sort can also be applied to the <c>_doc</c> field to sort by index order.
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor<TDocument> Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public QueryUserRequestDescriptor<TDocument> Sort(Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument> descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public QueryUserRequestDescriptor<TDocument> Sort(Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public QueryUserRequestDescriptor<TDocument> Sort(params Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor<TDocument>>[] configure)
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor<TDocument>(QueryDescriptorAction), options);
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
/// Find users with a query.
/// </para>
/// <para>
/// Get information for users in a paginated manner.
/// You can optionally filter the results with a query.
/// </para>
/// </summary>
public sealed partial class QueryUserRequestDescriptor : RequestDescriptor<QueryUserRequestDescriptor, QueryUserRequestParameters>
{
	internal QueryUserRequestDescriptor(Action<QueryUserRequestDescriptor> configure) => configure.Invoke(this);

	public QueryUserRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityQueryUser;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "security.query_user";

	public QueryUserRequestDescriptor WithProfileUid(bool? withProfileUid = true) => Qs("with_profile_uid", withProfileUid);

	private int? FromValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.UserQuery? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor> QueryDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.FieldValue>? SearchAfterValue { get; set; }
	private int? SizeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.SortOptions>? SortValue { get; set; }
	private Elastic.Clients.Elasticsearch.SortOptionsDescriptor SortDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor> SortDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor>[] SortDescriptorActions { get; set; }

	/// <summary>
	/// <para>
	/// Starting document offset.
	/// By default, you cannot page through more than 10,000 hits using the from and size parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor From(int? from)
	{
		FromValue = from;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A query to filter which users to return.
	/// If the query parameter is missing, it is equivalent to a <c>match_all</c> query.
	/// The query supports a subset of query types, including <c>match_all</c>, <c>bool</c>, <c>term</c>, <c>terms</c>, <c>match</c>,
	/// <c>ids</c>, <c>prefix</c>, <c>wildcard</c>, <c>exists</c>, <c>range</c>, and <c>simple_query_string</c>.
	/// You can query the following information associated with user: <c>username</c>, <c>roles</c>, <c>enabled</c>
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor Query(Elastic.Clients.Elasticsearch.Security.UserQuery? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public QueryUserRequestDescriptor Query(Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public QueryUserRequestDescriptor Query(Action<Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Search after definition
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor SearchAfter(ICollection<Elastic.Clients.Elasticsearch.FieldValue>? searchAfter)
	{
		SearchAfterValue = searchAfter;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The number of hits to return.
	/// By default, you cannot page through more than 10,000 hits using the <c>from</c> and <c>size</c> parameters.
	/// To page through more hits, use the <c>search_after</c> parameter.
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Fields eligible for sorting are: username, roles, enabled
	/// In addition, sort can also be applied to the <c>_doc</c> field to sort by index order.
	/// </para>
	/// </summary>
	public QueryUserRequestDescriptor Sort(ICollection<Elastic.Clients.Elasticsearch.SortOptions>? sort)
	{
		SortDescriptor = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortValue = sort;
		return Self;
	}

	public QueryUserRequestDescriptor Sort(Elastic.Clients.Elasticsearch.SortOptionsDescriptor descriptor)
	{
		SortValue = null;
		SortDescriptorAction = null;
		SortDescriptorActions = null;
		SortDescriptor = descriptor;
		return Self;
	}

	public QueryUserRequestDescriptor Sort(Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor> configure)
	{
		SortValue = null;
		SortDescriptor = null;
		SortDescriptorActions = null;
		SortDescriptorAction = configure;
		return Self;
	}

	public QueryUserRequestDescriptor Sort(params Action<Elastic.Clients.Elasticsearch.SortOptionsDescriptor>[] configure)
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Security.UserQueryDescriptor(QueryDescriptorAction), options);
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