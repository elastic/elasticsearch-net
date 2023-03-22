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

public sealed class TermsEnumRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>The terms enum API  can be used to discover terms in the index that begin with the provided string. It is designed for low-latency look-ups used in auto-complete scenarios.</para>
/// </summary>
public sealed partial class TermsEnumRequest : PlainRequest<TermsEnumRequestParameters>
{
	public TermsEnumRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceTermsEnum;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	/// <summary>
	/// <para>The string to match at the start of indexed terms. If not provided, all terms in the field are considered.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>How many matching terms to return.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("size")]
	public int? Size { get; set; }

	/// <summary>
	/// <para>The maximum length of time to spend collecting results. Defaults to "1s" (one second). If the timeout is exceeded the complete flag set to false in the response and the results may be partial or empty.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("timeout")]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get; set; }

	/// <summary>
	/// <para>When true the provided search string is matched against index terms without case sensitivity.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("case_insensitive")]
	public bool? CaseInsensitive { get; set; }

	/// <summary>
	/// <para>Allows to filter an index shard if the provided query rewrites to match_none.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("index_filter")]
	public Elastic.Clients.Elasticsearch.QueryDsl.Query? IndexFilter { get; set; }

	/// <summary>
	/// <para>The string after which terms in the index should be returned. Allows for a form of pagination if the last result from one request is passed as the search_after parameter for a subsequent request.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("string")]
	public string? String { get; set; }
	[JsonInclude, JsonPropertyName("search_after")]
	public string? SearchAfter { get; set; }
}

/// <summary>
/// <para>The terms enum API  can be used to discover terms in the index that begin with the provided string. It is designed for low-latency look-ups used in auto-complete scenarios.</para>
/// </summary>
public sealed partial class TermsEnumRequestDescriptor<TDocument> : RequestDescriptor<TermsEnumRequestDescriptor<TDocument>, TermsEnumRequestParameters>
{
	internal TermsEnumRequestDescriptor(Action<TermsEnumRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TermsEnumRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	public TermsEnumRequestDescriptor(TDocument document) : this(typeof(TDocument))
	{
	}

	internal TermsEnumRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceTermsEnum;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	public TermsEnumRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.Query? IndexFilterValue { get; set; }
	private QueryDsl.QueryDescriptor<TDocument> IndexFilterDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor<TDocument>> IndexFilterDescriptorAction { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? SearchAfterValue { get; set; }
	private int? SizeValue { get; set; }
	private string? StringValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? TimeoutValue { get; set; }

	/// <summary>
	/// <para>Allows to filter an index shard if the provided query rewrites to match_none.</para>
	/// </summary>
	public TermsEnumRequestDescriptor<TDocument> IndexFilter(Elastic.Clients.Elasticsearch.QueryDsl.Query? indexFilter)
	{
		IndexFilterDescriptor = null;
		IndexFilterDescriptorAction = null;
		IndexFilterValue = indexFilter;
		return Self;
	}

	public TermsEnumRequestDescriptor<TDocument> IndexFilter(QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		IndexFilterValue = null;
		IndexFilterDescriptorAction = null;
		IndexFilterDescriptor = descriptor;
		return Self;
	}

	public TermsEnumRequestDescriptor<TDocument> IndexFilter(Action<QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		IndexFilterValue = null;
		IndexFilterDescriptor = null;
		IndexFilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>When true the provided search string is matched against index terms without case sensitivity.</para>
	/// </summary>
	public TermsEnumRequestDescriptor<TDocument> CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	/// <summary>
	/// <para>The string to match at the start of indexed terms. If not provided, all terms in the field are considered.</para>
	/// </summary>
	public TermsEnumRequestDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The string to match at the start of indexed terms. If not provided, all terms in the field are considered.</para>
	/// </summary>
	public TermsEnumRequestDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsEnumRequestDescriptor<TDocument> SearchAfter(string? searchAfter)
	{
		SearchAfterValue = searchAfter;
		return Self;
	}

	/// <summary>
	/// <para>How many matching terms to return.</para>
	/// </summary>
	public TermsEnumRequestDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>The string after which terms in the index should be returned. Allows for a form of pagination if the last result from one request is passed as the search_after parameter for a subsequent request.</para>
	/// </summary>
	public TermsEnumRequestDescriptor<TDocument> String(string? value)
	{
		StringValue = value;
		return Self;
	}

	/// <summary>
	/// <para>The maximum length of time to spend collecting results. Defaults to "1s" (one second). If the timeout is exceeded the complete flag set to false in the response and the results may be partial or empty.</para>
	/// </summary>
	public TermsEnumRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout)
	{
		TimeoutValue = timeout;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (IndexFilterDescriptor is not null)
		{
			writer.WritePropertyName("index_filter");
			JsonSerializer.Serialize(writer, IndexFilterDescriptor, options);
		}
		else if (IndexFilterDescriptorAction is not null)
		{
			writer.WritePropertyName("index_filter");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor<TDocument>(IndexFilterDescriptorAction), options);
		}
		else if (IndexFilterValue is not null)
		{
			writer.WritePropertyName("index_filter");
			JsonSerializer.Serialize(writer, IndexFilterValue, options);
		}

		if (CaseInsensitiveValue.HasValue)
		{
			writer.WritePropertyName("case_insensitive");
			writer.WriteBooleanValue(CaseInsensitiveValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (!string.IsNullOrEmpty(SearchAfterValue))
		{
			writer.WritePropertyName("search_after");
			writer.WriteStringValue(SearchAfterValue);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (!string.IsNullOrEmpty(StringValue))
		{
			writer.WritePropertyName("string");
			writer.WriteStringValue(StringValue);
		}

		if (TimeoutValue is not null)
		{
			writer.WritePropertyName("timeout");
			JsonSerializer.Serialize(writer, TimeoutValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>The terms enum API  can be used to discover terms in the index that begin with the provided string. It is designed for low-latency look-ups used in auto-complete scenarios.</para>
/// </summary>
public sealed partial class TermsEnumRequestDescriptor : RequestDescriptor<TermsEnumRequestDescriptor, TermsEnumRequestParameters>
{
	internal TermsEnumRequestDescriptor(Action<TermsEnumRequestDescriptor> configure) => configure.Invoke(this);

	public TermsEnumRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal TermsEnumRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceTermsEnum;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	public TermsEnumRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.QueryDsl.Query? IndexFilterValue { get; set; }
	private QueryDsl.QueryDescriptor IndexFilterDescriptor { get; set; }
	private Action<QueryDsl.QueryDescriptor> IndexFilterDescriptorAction { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? SearchAfterValue { get; set; }
	private int? SizeValue { get; set; }
	private string? StringValue { get; set; }
	private Elastic.Clients.Elasticsearch.Duration? TimeoutValue { get; set; }

	/// <summary>
	/// <para>Allows to filter an index shard if the provided query rewrites to match_none.</para>
	/// </summary>
	public TermsEnumRequestDescriptor IndexFilter(Elastic.Clients.Elasticsearch.QueryDsl.Query? indexFilter)
	{
		IndexFilterDescriptor = null;
		IndexFilterDescriptorAction = null;
		IndexFilterValue = indexFilter;
		return Self;
	}

	public TermsEnumRequestDescriptor IndexFilter(QueryDsl.QueryDescriptor descriptor)
	{
		IndexFilterValue = null;
		IndexFilterDescriptorAction = null;
		IndexFilterDescriptor = descriptor;
		return Self;
	}

	public TermsEnumRequestDescriptor IndexFilter(Action<QueryDsl.QueryDescriptor> configure)
	{
		IndexFilterValue = null;
		IndexFilterDescriptor = null;
		IndexFilterDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>When true the provided search string is matched against index terms without case sensitivity.</para>
	/// </summary>
	public TermsEnumRequestDescriptor CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	/// <summary>
	/// <para>The string to match at the start of indexed terms. If not provided, all terms in the field are considered.</para>
	/// </summary>
	public TermsEnumRequestDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The string to match at the start of indexed terms. If not provided, all terms in the field are considered.</para>
	/// </summary>
	public TermsEnumRequestDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The string to match at the start of indexed terms. If not provided, all terms in the field are considered.</para>
	/// </summary>
	public TermsEnumRequestDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsEnumRequestDescriptor SearchAfter(string? searchAfter)
	{
		SearchAfterValue = searchAfter;
		return Self;
	}

	/// <summary>
	/// <para>How many matching terms to return.</para>
	/// </summary>
	public TermsEnumRequestDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	/// <summary>
	/// <para>The string after which terms in the index should be returned. Allows for a form of pagination if the last result from one request is passed as the search_after parameter for a subsequent request.</para>
	/// </summary>
	public TermsEnumRequestDescriptor String(string? value)
	{
		StringValue = value;
		return Self;
	}

	/// <summary>
	/// <para>The maximum length of time to spend collecting results. Defaults to "1s" (one second). If the timeout is exceeded the complete flag set to false in the response and the results may be partial or empty.</para>
	/// </summary>
	public TermsEnumRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout)
	{
		TimeoutValue = timeout;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (IndexFilterDescriptor is not null)
		{
			writer.WritePropertyName("index_filter");
			JsonSerializer.Serialize(writer, IndexFilterDescriptor, options);
		}
		else if (IndexFilterDescriptorAction is not null)
		{
			writer.WritePropertyName("index_filter");
			JsonSerializer.Serialize(writer, new QueryDsl.QueryDescriptor(IndexFilterDescriptorAction), options);
		}
		else if (IndexFilterValue is not null)
		{
			writer.WritePropertyName("index_filter");
			JsonSerializer.Serialize(writer, IndexFilterValue, options);
		}

		if (CaseInsensitiveValue.HasValue)
		{
			writer.WritePropertyName("case_insensitive");
			writer.WriteBooleanValue(CaseInsensitiveValue.Value);
		}

		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		if (!string.IsNullOrEmpty(SearchAfterValue))
		{
			writer.WritePropertyName("search_after");
			writer.WriteStringValue(SearchAfterValue);
		}

		if (SizeValue.HasValue)
		{
			writer.WritePropertyName("size");
			writer.WriteNumberValue(SizeValue.Value);
		}

		if (!string.IsNullOrEmpty(StringValue))
		{
			writer.WritePropertyName("string");
			writer.WriteStringValue(StringValue);
		}

		if (TimeoutValue is not null)
		{
			writer.WritePropertyName("timeout");
			JsonSerializer.Serialize(writer, TimeoutValue, options);
		}

		writer.WriteEndObject();
	}
}