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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;

public sealed partial class SimpleQueryStringQuery
{
	/// <summary>
	/// <para>Analyzer used to convert text in the query string into tokens.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("analyzer")]
	public string? Analyzer { get; set; }

	/// <summary>
	/// <para>If `true`, the query attempts to analyze wildcard terms in the query string.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("analyze_wildcard")]
	public bool? AnalyzeWildcard { get; set; }

	/// <summary>
	/// <para>If `true`, the parser creates a match_phrase query for each multi-position token.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("auto_generate_synonyms_phrase_query")]
	public bool? AutoGenerateSynonymsPhraseQuery { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }

	/// <summary>
	/// <para>Default boolean logic used to interpret text in the query string if no operators are specified.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("default_operator")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? DefaultOperator { get; set; }

	/// <summary>
	/// <para>Array of fields you wish to search.<br/>Accepts wildcard expressions.<br/>You also can boost relevance scores for matches to particular fields using a caret (`^`) notation.<br/>Defaults to the `index.query.default_field index` setting, which has a default value of `*`.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("fields")]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.Field>? Fields { get; set; }

	/// <summary>
	/// <para>List of enabled operators for the simple query string syntax.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("flags")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SimpleQueryStringFlag? Flags { get; set; }

	/// <summary>
	/// <para>Maximum number of terms to which the query expands for fuzzy matching.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("fuzzy_max_expansions")]
	public int? FuzzyMaxExpansions { get; set; }

	/// <summary>
	/// <para>Number of beginning characters left unchanged for fuzzy matching.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("fuzzy_prefix_length")]
	public int? FuzzyPrefixLength { get; set; }

	/// <summary>
	/// <para>If `true`, edits for fuzzy matching include transpositions of two adjacent characters (for example, `ab` to `ba`).</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("fuzzy_transpositions")]
	public bool? FuzzyTranspositions { get; set; }

	/// <summary>
	/// <para>If `true`, format-based errors, such as providing a text value for a numeric field, are ignored.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("lenient")]
	public bool? Lenient { get; set; }

	/// <summary>
	/// <para>Minimum number of clauses that must match for a document to be returned.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("minimum_should_match")]
	public Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? MinimumShouldMatch { get; set; }

	/// <summary>
	/// <para>Query string in the simple query string syntax you wish to parse and use for search.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public string Query { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>Suffix appended to quoted text in the query string.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("quote_field_suffix")]
	public string? QuoteFieldSuffix { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query(SimpleQueryStringQuery simpleQueryStringQuery) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query.SimpleQueryString(simpleQueryStringQuery);
}

public sealed partial class SimpleQueryStringQueryDescriptor<TDocument> : SerializableDescriptor<SimpleQueryStringQueryDescriptor<TDocument>>
{
	internal SimpleQueryStringQueryDescriptor(Action<SimpleQueryStringQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SimpleQueryStringQueryDescriptor() : base()
	{
	}

	private string? AnalyzerValue { get; set; }
	private bool? AnalyzeWildcardValue { get; set; }
	private bool? AutoGenerateSynonymsPhraseQueryValue { get; set; }
	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? DefaultOperatorValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Field>? FieldsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SimpleQueryStringFlag? FlagsValue { get; set; }
	private int? FuzzyMaxExpansionsValue { get; set; }
	private int? FuzzyPrefixLengthValue { get; set; }
	private bool? FuzzyTranspositionsValue { get; set; }
	private bool? LenientValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private string QueryValue { get; set; }
	private string? QueryNameValue { get; set; }
	private string? QuoteFieldSuffixValue { get; set; }

	/// <summary>
	/// <para>Analyzer used to convert text in the query string into tokens.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, the query attempts to analyze wildcard terms in the query string.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> AnalyzeWildcard(bool? analyzeWildcard = true)
	{
		AnalyzeWildcardValue = analyzeWildcard;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, the parser creates a match_phrase query for each multi-position token.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true)
	{
		AutoGenerateSynonymsPhraseQueryValue = autoGenerateSynonymsPhraseQuery;
		return Self;
	}

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>Default boolean logic used to interpret text in the query string if no operators are specified.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> DefaultOperator(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? defaultOperator)
	{
		DefaultOperatorValue = defaultOperator;
		return Self;
	}

	/// <summary>
	/// <para>Array of fields you wish to search.<br/>Accepts wildcard expressions.<br/>You also can boost relevance scores for matches to particular fields using a caret (`^`) notation.<br/>Defaults to the `index.query.default_field index` setting, which has a default value of `*`.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> Fields(ICollection<Elastic.Clients.Elasticsearch.Serverless.Field>? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	/// <summary>
	/// <para>List of enabled operators for the simple query string syntax.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> Flags(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SimpleQueryStringFlag? flags)
	{
		FlagsValue = flags;
		return Self;
	}

	/// <summary>
	/// <para>Maximum number of terms to which the query expands for fuzzy matching.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> FuzzyMaxExpansions(int? fuzzyMaxExpansions)
	{
		FuzzyMaxExpansionsValue = fuzzyMaxExpansions;
		return Self;
	}

	/// <summary>
	/// <para>Number of beginning characters left unchanged for fuzzy matching.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> FuzzyPrefixLength(int? fuzzyPrefixLength)
	{
		FuzzyPrefixLengthValue = fuzzyPrefixLength;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, edits for fuzzy matching include transpositions of two adjacent characters (for example, `ab` to `ba`).</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> FuzzyTranspositions(bool? fuzzyTranspositions = true)
	{
		FuzzyTranspositionsValue = fuzzyTranspositions;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, format-based errors, such as providing a text value for a numeric field, are ignored.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> Lenient(bool? lenient = true)
	{
		LenientValue = lenient;
		return Self;
	}

	/// <summary>
	/// <para>Minimum number of clauses that must match for a document to be returned.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> MinimumShouldMatch(Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	/// <summary>
	/// <para>Query string in the simple query string syntax you wish to parse and use for search.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> Query(string query)
	{
		QueryValue = query;
		return Self;
	}

	public SimpleQueryStringQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>Suffix appended to quoted text in the query string.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor<TDocument> QuoteFieldSuffix(string? quoteFieldSuffix)
	{
		QuoteFieldSuffixValue = quoteFieldSuffix;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		if (AnalyzeWildcardValue.HasValue)
		{
			writer.WritePropertyName("analyze_wildcard");
			writer.WriteBooleanValue(AnalyzeWildcardValue.Value);
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

		if (DefaultOperatorValue is not null)
		{
			writer.WritePropertyName("default_operator");
			JsonSerializer.Serialize(writer, DefaultOperatorValue, options);
		}

		if (FieldsValue is not null)
		{
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
		}

		if (FlagsValue is not null)
		{
			writer.WritePropertyName("flags");
			JsonSerializer.Serialize(writer, FlagsValue, options);
		}

		if (FuzzyMaxExpansionsValue.HasValue)
		{
			writer.WritePropertyName("fuzzy_max_expansions");
			writer.WriteNumberValue(FuzzyMaxExpansionsValue.Value);
		}

		if (FuzzyPrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("fuzzy_prefix_length");
			writer.WriteNumberValue(FuzzyPrefixLengthValue.Value);
		}

		if (FuzzyTranspositionsValue.HasValue)
		{
			writer.WritePropertyName("fuzzy_transpositions");
			writer.WriteBooleanValue(FuzzyTranspositionsValue.Value);
		}

		if (LenientValue.HasValue)
		{
			writer.WritePropertyName("lenient");
			writer.WriteBooleanValue(LenientValue.Value);
		}

		if (MinimumShouldMatchValue is not null)
		{
			writer.WritePropertyName("minimum_should_match");
			JsonSerializer.Serialize(writer, MinimumShouldMatchValue, options);
		}

		writer.WritePropertyName("query");
		writer.WriteStringValue(QueryValue);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (!string.IsNullOrEmpty(QuoteFieldSuffixValue))
		{
			writer.WritePropertyName("quote_field_suffix");
			writer.WriteStringValue(QuoteFieldSuffixValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class SimpleQueryStringQueryDescriptor : SerializableDescriptor<SimpleQueryStringQueryDescriptor>
{
	internal SimpleQueryStringQueryDescriptor(Action<SimpleQueryStringQueryDescriptor> configure) => configure.Invoke(this);

	public SimpleQueryStringQueryDescriptor() : base()
	{
	}

	private string? AnalyzerValue { get; set; }
	private bool? AnalyzeWildcardValue { get; set; }
	private bool? AutoGenerateSynonymsPhraseQueryValue { get; set; }
	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? DefaultOperatorValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.Field>? FieldsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SimpleQueryStringFlag? FlagsValue { get; set; }
	private int? FuzzyMaxExpansionsValue { get; set; }
	private int? FuzzyPrefixLengthValue { get; set; }
	private bool? FuzzyTranspositionsValue { get; set; }
	private bool? LenientValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private string QueryValue { get; set; }
	private string? QueryNameValue { get; set; }
	private string? QuoteFieldSuffixValue { get; set; }

	/// <summary>
	/// <para>Analyzer used to convert text in the query string into tokens.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, the query attempts to analyze wildcard terms in the query string.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor AnalyzeWildcard(bool? analyzeWildcard = true)
	{
		AnalyzeWildcardValue = analyzeWildcard;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, the parser creates a match_phrase query for each multi-position token.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true)
	{
		AutoGenerateSynonymsPhraseQueryValue = autoGenerateSynonymsPhraseQuery;
		return Self;
	}

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>Default boolean logic used to interpret text in the query string if no operators are specified.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor DefaultOperator(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? defaultOperator)
	{
		DefaultOperatorValue = defaultOperator;
		return Self;
	}

	/// <summary>
	/// <para>Array of fields you wish to search.<br/>Accepts wildcard expressions.<br/>You also can boost relevance scores for matches to particular fields using a caret (`^`) notation.<br/>Defaults to the `index.query.default_field index` setting, which has a default value of `*`.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor Fields(ICollection<Elastic.Clients.Elasticsearch.Serverless.Field>? fields)
	{
		FieldsValue = fields;
		return Self;
	}

	/// <summary>
	/// <para>List of enabled operators for the simple query string syntax.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor Flags(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SimpleQueryStringFlag? flags)
	{
		FlagsValue = flags;
		return Self;
	}

	/// <summary>
	/// <para>Maximum number of terms to which the query expands for fuzzy matching.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor FuzzyMaxExpansions(int? fuzzyMaxExpansions)
	{
		FuzzyMaxExpansionsValue = fuzzyMaxExpansions;
		return Self;
	}

	/// <summary>
	/// <para>Number of beginning characters left unchanged for fuzzy matching.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor FuzzyPrefixLength(int? fuzzyPrefixLength)
	{
		FuzzyPrefixLengthValue = fuzzyPrefixLength;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, edits for fuzzy matching include transpositions of two adjacent characters (for example, `ab` to `ba`).</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor FuzzyTranspositions(bool? fuzzyTranspositions = true)
	{
		FuzzyTranspositionsValue = fuzzyTranspositions;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, format-based errors, such as providing a text value for a numeric field, are ignored.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor Lenient(bool? lenient = true)
	{
		LenientValue = lenient;
		return Self;
	}

	/// <summary>
	/// <para>Minimum number of clauses that must match for a document to be returned.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor MinimumShouldMatch(Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	/// <summary>
	/// <para>Query string in the simple query string syntax you wish to parse and use for search.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor Query(string query)
	{
		QueryValue = query;
		return Self;
	}

	public SimpleQueryStringQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>Suffix appended to quoted text in the query string.</para>
	/// </summary>
	public SimpleQueryStringQueryDescriptor QuoteFieldSuffix(string? quoteFieldSuffix)
	{
		QuoteFieldSuffixValue = quoteFieldSuffix;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		if (AnalyzeWildcardValue.HasValue)
		{
			writer.WritePropertyName("analyze_wildcard");
			writer.WriteBooleanValue(AnalyzeWildcardValue.Value);
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

		if (DefaultOperatorValue is not null)
		{
			writer.WritePropertyName("default_operator");
			JsonSerializer.Serialize(writer, DefaultOperatorValue, options);
		}

		if (FieldsValue is not null)
		{
			writer.WritePropertyName("fields");
			JsonSerializer.Serialize(writer, FieldsValue, options);
		}

		if (FlagsValue is not null)
		{
			writer.WritePropertyName("flags");
			JsonSerializer.Serialize(writer, FlagsValue, options);
		}

		if (FuzzyMaxExpansionsValue.HasValue)
		{
			writer.WritePropertyName("fuzzy_max_expansions");
			writer.WriteNumberValue(FuzzyMaxExpansionsValue.Value);
		}

		if (FuzzyPrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("fuzzy_prefix_length");
			writer.WriteNumberValue(FuzzyPrefixLengthValue.Value);
		}

		if (FuzzyTranspositionsValue.HasValue)
		{
			writer.WritePropertyName("fuzzy_transpositions");
			writer.WriteBooleanValue(FuzzyTranspositionsValue.Value);
		}

		if (LenientValue.HasValue)
		{
			writer.WritePropertyName("lenient");
			writer.WriteBooleanValue(LenientValue.Value);
		}

		if (MinimumShouldMatchValue is not null)
		{
			writer.WritePropertyName("minimum_should_match");
			JsonSerializer.Serialize(writer, MinimumShouldMatchValue, options);
		}

		writer.WritePropertyName("query");
		writer.WriteStringValue(QueryValue);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (!string.IsNullOrEmpty(QuoteFieldSuffixValue))
		{
			writer.WritePropertyName("quote_field_suffix");
			writer.WriteStringValue(QuoteFieldSuffixValue);
		}

		writer.WriteEndObject();
	}
}