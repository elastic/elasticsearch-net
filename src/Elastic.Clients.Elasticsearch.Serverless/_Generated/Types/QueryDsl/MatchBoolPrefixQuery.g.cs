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

internal sealed partial class MatchBoolPrefixQueryConverter : JsonConverter<MatchBoolPrefixQuery>
{
	public override MatchBoolPrefixQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		reader.Read();
		var fieldName = reader.GetString();
		reader.Read();
		var variant = new MatchBoolPrefixQuery(fieldName);
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var property = reader.GetString();
				if (property == "analyzer")
				{
					variant.Analyzer = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "boost")
				{
					variant.Boost = JsonSerializer.Deserialize<float?>(ref reader, options);
					continue;
				}

				if (property == "fuzziness")
				{
					variant.Fuzziness = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.Fuzziness?>(ref reader, options);
					continue;
				}

				if (property == "fuzzy_rewrite")
				{
					variant.FuzzyRewrite = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "fuzzy_transpositions")
				{
					variant.FuzzyTranspositions = JsonSerializer.Deserialize<bool?>(ref reader, options);
					continue;
				}

				if (property == "max_expansions")
				{
					variant.MaxExpansions = JsonSerializer.Deserialize<int?>(ref reader, options);
					continue;
				}

				if (property == "minimum_should_match")
				{
					variant.MinimumShouldMatch = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch?>(ref reader, options);
					continue;
				}

				if (property == "operator")
				{
					variant.Operator = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator?>(ref reader, options);
					continue;
				}

				if (property == "prefix_length")
				{
					variant.PrefixLength = JsonSerializer.Deserialize<int?>(ref reader, options);
					continue;
				}

				if (property == "query")
				{
					variant.Query = JsonSerializer.Deserialize<string>(ref reader, options);
					continue;
				}

				if (property == "_name")
				{
					variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}
			}
		}

		reader.Read();
		return variant;
	}

	public override void Write(Utf8JsonWriter writer, MatchBoolPrefixQuery value, JsonSerializerOptions options)
	{
		if (value.Field is null)
			throw new JsonException("Unable to serialize MatchBoolPrefixQuery because the `Field` property is not set. Field name queries must include a valid field name.");
		if (options.TryGetClientSettings(out var settings))
		{
			writer.WriteStartObject();
			writer.WritePropertyName(settings.Inferrer.Field(value.Field));
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(value.Analyzer))
			{
				writer.WritePropertyName("analyzer");
				writer.WriteStringValue(value.Analyzer);
			}

			if (value.Boost.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(value.Boost.Value);
			}

			if (value.Fuzziness is not null)
			{
				writer.WritePropertyName("fuzziness");
				JsonSerializer.Serialize(writer, value.Fuzziness, options);
			}

			if (value.FuzzyRewrite is not null)
			{
				writer.WritePropertyName("fuzzy_rewrite");
				JsonSerializer.Serialize(writer, value.FuzzyRewrite, options);
			}

			if (value.FuzzyTranspositions.HasValue)
			{
				writer.WritePropertyName("fuzzy_transpositions");
				writer.WriteBooleanValue(value.FuzzyTranspositions.Value);
			}

			if (value.MaxExpansions.HasValue)
			{
				writer.WritePropertyName("max_expansions");
				writer.WriteNumberValue(value.MaxExpansions.Value);
			}

			if (value.MinimumShouldMatch is not null)
			{
				writer.WritePropertyName("minimum_should_match");
				JsonSerializer.Serialize(writer, value.MinimumShouldMatch, options);
			}

			if (value.Operator is not null)
			{
				writer.WritePropertyName("operator");
				JsonSerializer.Serialize(writer, value.Operator, options);
			}

			if (value.PrefixLength.HasValue)
			{
				writer.WritePropertyName("prefix_length");
				writer.WriteNumberValue(value.PrefixLength.Value);
			}

			writer.WritePropertyName("query");
			writer.WriteStringValue(value.Query);
			if (!string.IsNullOrEmpty(value.QueryName))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(value.QueryName);
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
			return;
		}

		throw new JsonException("Unable to retrieve client settings required to infer field.");
	}
}

[JsonConverter(typeof(MatchBoolPrefixQueryConverter))]
public sealed partial class MatchBoolPrefixQuery : SearchQuery
{
	public MatchBoolPrefixQuery(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
	}

	public string? QueryName { get; set; }

	/// <summary>
	/// <para>Analyzer used to convert the text in the query value into tokens.</para>
	/// </summary>
	public string? Analyzer { get; set; }
	public float? Boost { get; set; }

	/// <summary>
	/// <para>Maximum edit distance allowed for matching.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Fuzziness? Fuzziness { get; set; }

	/// <summary>
	/// <para>Method used to rewrite the query.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public string? FuzzyRewrite { get; set; }

	/// <summary>
	/// <para>If `true`, edits for fuzzy matching include transpositions of two adjacent characters (for example, `ab` to `ba`).<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public bool? FuzzyTranspositions { get; set; }

	/// <summary>
	/// <para>Maximum number of terms to which the query will expand.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public int? MaxExpansions { get; set; }

	/// <summary>
	/// <para>Minimum number of clauses that must match for a document to be returned.<br/>Applied to the constructed bool query.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? MinimumShouldMatch { get; set; }

	/// <summary>
	/// <para>Boolean logic used to interpret text in the query value.<br/>Applied to the constructed bool query.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? Operator { get; set; }

	/// <summary>
	/// <para>Number of beginning characters left unchanged for fuzzy matching.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public int? PrefixLength { get; set; }

	/// <summary>
	/// <para>Terms you wish to find in the provided field.<br/>The last term is used in a prefix query.</para>
	/// </summary>
	public string Query { get; set; }
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	public static implicit operator Query(MatchBoolPrefixQuery matchBoolPrefixQuery) => QueryDsl.Query.MatchBoolPrefix(matchBoolPrefixQuery);

	internal override void InternalWrapInContainer(Query container) => container.WrapVariant("match_bool_prefix", this);
}

public sealed partial class MatchBoolPrefixQueryDescriptor<TDocument> : SerializableDescriptor<MatchBoolPrefixQueryDescriptor<TDocument>>
{
	internal MatchBoolPrefixQueryDescriptor(Action<MatchBoolPrefixQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	internal MatchBoolPrefixQueryDescriptor() : base()
	{
	}

	public MatchBoolPrefixQueryDescriptor(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	public MatchBoolPrefixQueryDescriptor(Expression<Func<TDocument, object>> field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	private string? AnalyzerValue { get; set; }
	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Fuzziness? FuzzinessValue { get; set; }
	private string? FuzzyRewriteValue { get; set; }
	private bool? FuzzyTranspositionsValue { get; set; }
	private int? MaxExpansionsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? OperatorValue { get; set; }
	private int? PrefixLengthValue { get; set; }
	private string QueryValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>Analyzer used to convert the text in the query value into tokens.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Maximum edit distance allowed for matching.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> Fuzziness(Elastic.Clients.Elasticsearch.Serverless.Fuzziness? fuzziness)
	{
		FuzzinessValue = fuzziness;
		return Self;
	}

	/// <summary>
	/// <para>Method used to rewrite the query.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> FuzzyRewrite(string? fuzzyRewrite)
	{
		FuzzyRewriteValue = fuzzyRewrite;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, edits for fuzzy matching include transpositions of two adjacent characters (for example, `ab` to `ba`).<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> FuzzyTranspositions(bool? fuzzyTranspositions = true)
	{
		FuzzyTranspositionsValue = fuzzyTranspositions;
		return Self;
	}

	/// <summary>
	/// <para>Maximum number of terms to which the query will expand.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> MaxExpansions(int? maxExpansions)
	{
		MaxExpansionsValue = maxExpansions;
		return Self;
	}

	/// <summary>
	/// <para>Minimum number of clauses that must match for a document to be returned.<br/>Applied to the constructed bool query.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> MinimumShouldMatch(Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	/// <summary>
	/// <para>Boolean logic used to interpret text in the query value.<br/>Applied to the constructed bool query.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> Operator(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? op)
	{
		OperatorValue = op;
		return Self;
	}

	/// <summary>
	/// <para>Number of beginning characters left unchanged for fuzzy matching.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> PrefixLength(int? prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	/// <summary>
	/// <para>Terms you wish to find in the provided field.<br/>The last term is used in a prefix query.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor<TDocument> Query(string query)
	{
		QueryValue = query;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (FieldValue is null)
			throw new JsonException("Unable to serialize field name query descriptor with a null field. Ensure you use a suitable descriptor constructor or call the Field method, passing a non-null value for the field argument.");
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (FuzzinessValue is not null)
		{
			writer.WritePropertyName("fuzziness");
			JsonSerializer.Serialize(writer, FuzzinessValue, options);
		}

		if (FuzzyRewriteValue is not null)
		{
			writer.WritePropertyName("fuzzy_rewrite");
			JsonSerializer.Serialize(writer, FuzzyRewriteValue, options);
		}

		if (FuzzyTranspositionsValue.HasValue)
		{
			writer.WritePropertyName("fuzzy_transpositions");
			writer.WriteBooleanValue(FuzzyTranspositionsValue.Value);
		}

		if (MaxExpansionsValue.HasValue)
		{
			writer.WritePropertyName("max_expansions");
			writer.WriteNumberValue(MaxExpansionsValue.Value);
		}

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

		if (PrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("prefix_length");
			writer.WriteNumberValue(PrefixLengthValue.Value);
		}

		writer.WritePropertyName("query");
		writer.WriteStringValue(QueryValue);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public sealed partial class MatchBoolPrefixQueryDescriptor : SerializableDescriptor<MatchBoolPrefixQueryDescriptor>
{
	internal MatchBoolPrefixQueryDescriptor(Action<MatchBoolPrefixQueryDescriptor> configure) => configure.Invoke(this);

	internal MatchBoolPrefixQueryDescriptor() : base()
	{
	}

	public MatchBoolPrefixQueryDescriptor(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	private string? AnalyzerValue { get; set; }
	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Fuzziness? FuzzinessValue { get; set; }
	private string? FuzzyRewriteValue { get; set; }
	private bool? FuzzyTranspositionsValue { get; set; }
	private int? MaxExpansionsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? OperatorValue { get; set; }
	private int? PrefixLengthValue { get; set; }
	private string QueryValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>Analyzer used to convert the text in the query value into tokens.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor Analyzer(string? analyzer)
	{
		AnalyzerValue = analyzer;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Maximum edit distance allowed for matching.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor Fuzziness(Elastic.Clients.Elasticsearch.Serverless.Fuzziness? fuzziness)
	{
		FuzzinessValue = fuzziness;
		return Self;
	}

	/// <summary>
	/// <para>Method used to rewrite the query.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor FuzzyRewrite(string? fuzzyRewrite)
	{
		FuzzyRewriteValue = fuzzyRewrite;
		return Self;
	}

	/// <summary>
	/// <para>If `true`, edits for fuzzy matching include transpositions of two adjacent characters (for example, `ab` to `ba`).<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor FuzzyTranspositions(bool? fuzzyTranspositions = true)
	{
		FuzzyTranspositionsValue = fuzzyTranspositions;
		return Self;
	}

	/// <summary>
	/// <para>Maximum number of terms to which the query will expand.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor MaxExpansions(int? maxExpansions)
	{
		MaxExpansionsValue = maxExpansions;
		return Self;
	}

	/// <summary>
	/// <para>Minimum number of clauses that must match for a document to be returned.<br/>Applied to the constructed bool query.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor MinimumShouldMatch(Elastic.Clients.Elasticsearch.Serverless.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	/// <summary>
	/// <para>Boolean logic used to interpret text in the query value.<br/>Applied to the constructed bool query.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor Operator(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Operator? op)
	{
		OperatorValue = op;
		return Self;
	}

	/// <summary>
	/// <para>Number of beginning characters left unchanged for fuzzy matching.<br/>Can be applied to the term subqueries constructed for all terms but the final term.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor PrefixLength(int? prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	/// <summary>
	/// <para>Terms you wish to find in the provided field.<br/>The last term is used in a prefix query.</para>
	/// </summary>
	public MatchBoolPrefixQueryDescriptor Query(string query)
	{
		QueryValue = query;
		return Self;
	}

	public MatchBoolPrefixQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (FieldValue is null)
			throw new JsonException("Unable to serialize field name query descriptor with a null field. Ensure you use a suitable descriptor constructor or call the Field method, passing a non-null value for the field argument.");
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(AnalyzerValue))
		{
			writer.WritePropertyName("analyzer");
			writer.WriteStringValue(AnalyzerValue);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (FuzzinessValue is not null)
		{
			writer.WritePropertyName("fuzziness");
			JsonSerializer.Serialize(writer, FuzzinessValue, options);
		}

		if (FuzzyRewriteValue is not null)
		{
			writer.WritePropertyName("fuzzy_rewrite");
			JsonSerializer.Serialize(writer, FuzzyRewriteValue, options);
		}

		if (FuzzyTranspositionsValue.HasValue)
		{
			writer.WritePropertyName("fuzzy_transpositions");
			writer.WriteBooleanValue(FuzzyTranspositionsValue.Value);
		}

		if (MaxExpansionsValue.HasValue)
		{
			writer.WritePropertyName("max_expansions");
			writer.WriteNumberValue(MaxExpansionsValue.Value);
		}

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

		if (PrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("prefix_length");
			writer.WriteNumberValue(PrefixLengthValue.Value);
		}

		writer.WritePropertyName("query");
		writer.WriteStringValue(QueryValue);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}