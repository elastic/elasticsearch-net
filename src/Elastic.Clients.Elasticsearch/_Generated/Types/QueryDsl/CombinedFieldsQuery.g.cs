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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class CombinedFieldsQuery : SearchQuery
{
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }
	[JsonInclude, JsonPropertyName("auto_generate_synonyms_phrase_query")]
	public bool? AutoGenerateSynonymsPhraseQuery { get; set; }
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }
	[JsonInclude, JsonPropertyName("fields")]
	public Fields Fields { get; set; }
	[JsonInclude, JsonPropertyName("minimum_should_match")]
	public Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatch { get; set; }
	[JsonInclude, JsonPropertyName("operator")]
	public Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsOperator? Operator { get; set; }
	[JsonInclude, JsonPropertyName("query")]
	public string Query { get; set; }
	[JsonInclude, JsonPropertyName("zero_terms_query")]
	public Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsZeroTerms? ZeroTermsQuery { get; set; }

	public static implicit operator Query(CombinedFieldsQuery combinedFieldsQuery) => QueryDsl.Query.CombinedFields(combinedFieldsQuery);

	internal override void InternalWrapInContainer(Query container) => container.WrapVariant("combined_fields", this);
}

public sealed partial class CombinedFieldsQueryDescriptor<TDocument> : SerializableDescriptor<CombinedFieldsQueryDescriptor<TDocument>>
{
	internal CombinedFieldsQueryDescriptor(Action<CombinedFieldsQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public CombinedFieldsQueryDescriptor() : base()
	{
	}

	private string? QueryNameValue { get; set; }
	private bool? AutoGenerateSynonymsPhraseQueryValue { get; set; }
	private float? BoostValue { get; set; }
	private Fields FieldsValue { get; set; }
	private Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsOperator? OperatorValue { get; set; }
	private string QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsZeroTerms? ZeroTermsQueryValue { get; set; }

	public CombinedFieldsQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public CombinedFieldsQueryDescriptor<TDocument> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true)
	{
		AutoGenerateSynonymsPhraseQueryValue = autoGenerateSynonymsPhraseQuery;
		return Self;
	}

	public CombinedFieldsQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public CombinedFieldsQueryDescriptor<TDocument> Fields(Fields fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public CombinedFieldsQueryDescriptor<TDocument> MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	public CombinedFieldsQueryDescriptor<TDocument> Operator(Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsOperator? op)
	{
		OperatorValue = op;
		return Self;
	}

	public CombinedFieldsQueryDescriptor<TDocument> Query(string query)
	{
		QueryValue = query;
		return Self;
	}

	public CombinedFieldsQueryDescriptor<TDocument> ZeroTermsQuery(Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsZeroTerms? zeroTermsQuery)
	{
		ZeroTermsQueryValue = zeroTermsQuery;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (AutoGenerateSynonymsPhraseQueryValue.HasValue)
		{
			writer.WritePropertyName("auto_generate_synonyms_phrase_query");
			writer.WriteBooleanValue(AutoGenerateSynonymsPhraseQueryValue.Value);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		writer.WritePropertyName("fields");
		JsonSerializer.Serialize(writer, FieldsValue, options);
		if (MinimumShouldMatchValue is not null)
		{
			writer.WritePropertyName("minimum_should_match");
			JsonSerializer.Serialize(writer, MinimumShouldMatchValue, options);
		}

		if (OperatorValue is not null)
		{
			writer.WritePropertyName("operator");
			JsonSerializer.Serialize(writer, OperatorValue, options);
		}

		writer.WritePropertyName("query");
		writer.WriteStringValue(QueryValue);
		if (ZeroTermsQueryValue is not null)
		{
			writer.WritePropertyName("zero_terms_query");
			JsonSerializer.Serialize(writer, ZeroTermsQueryValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class CombinedFieldsQueryDescriptor : SerializableDescriptor<CombinedFieldsQueryDescriptor>
{
	internal CombinedFieldsQueryDescriptor(Action<CombinedFieldsQueryDescriptor> configure) => configure.Invoke(this);

	public CombinedFieldsQueryDescriptor() : base()
	{
	}

	private string? QueryNameValue { get; set; }
	private bool? AutoGenerateSynonymsPhraseQueryValue { get; set; }
	private float? BoostValue { get; set; }
	private Fields FieldsValue { get; set; }
	private Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsOperator? OperatorValue { get; set; }
	private string QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsZeroTerms? ZeroTermsQueryValue { get; set; }

	public CombinedFieldsQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public CombinedFieldsQueryDescriptor AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true)
	{
		AutoGenerateSynonymsPhraseQueryValue = autoGenerateSynonymsPhraseQuery;
		return Self;
	}

	public CombinedFieldsQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public CombinedFieldsQueryDescriptor Fields(Fields fields)
	{
		FieldsValue = fields;
		return Self;
	}

	public CombinedFieldsQueryDescriptor MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	public CombinedFieldsQueryDescriptor Operator(Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsOperator? op)
	{
		OperatorValue = op;
		return Self;
	}

	public CombinedFieldsQueryDescriptor Query(string query)
	{
		QueryValue = query;
		return Self;
	}

	public CombinedFieldsQueryDescriptor ZeroTermsQuery(Elastic.Clients.Elasticsearch.QueryDsl.CombinedFieldsZeroTerms? zeroTermsQuery)
	{
		ZeroTermsQueryValue = zeroTermsQuery;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (AutoGenerateSynonymsPhraseQueryValue.HasValue)
		{
			writer.WritePropertyName("auto_generate_synonyms_phrase_query");
			writer.WriteBooleanValue(AutoGenerateSynonymsPhraseQueryValue.Value);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		writer.WritePropertyName("fields");
		JsonSerializer.Serialize(writer, FieldsValue, options);
		if (MinimumShouldMatchValue is not null)
		{
			writer.WritePropertyName("minimum_should_match");
			JsonSerializer.Serialize(writer, MinimumShouldMatchValue, options);
		}

		if (OperatorValue is not null)
		{
			writer.WritePropertyName("operator");
			JsonSerializer.Serialize(writer, OperatorValue, options);
		}

		writer.WritePropertyName("query");
		writer.WriteStringValue(QueryValue);
		if (ZeroTermsQueryValue is not null)
		{
			writer.WritePropertyName("zero_terms_query");
			JsonSerializer.Serialize(writer, ZeroTermsQueryValue, options);
		}

		writer.WriteEndObject();
	}
}