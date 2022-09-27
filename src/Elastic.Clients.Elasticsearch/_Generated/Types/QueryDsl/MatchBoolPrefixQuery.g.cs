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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	internal sealed class MatchBoolPrefixQueryConverter : JsonConverter<MatchBoolPrefixQuery>
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
					if (property == "_name")
					{
						variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

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
						variant.Fuzziness = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Fuzziness?>(ref reader, options);
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
						variant.MinimumShouldMatch = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.MinimumShouldMatch?>(ref reader, options);
						continue;
					}

					if (property == "operator")
					{
						variant.Operator = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.Operator?>(ref reader, options);
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
				if (!string.IsNullOrEmpty(value.QueryName))
				{
					writer.WritePropertyName("_name");
					writer.WriteStringValue(value.QueryName);
				}

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
				writer.WriteEndObject();
				writer.WriteEndObject();
				return;
			}

			throw new JsonException("Unable to retrieve client settings required to infer field.");
		}
	}

	[JsonConverter(typeof(MatchBoolPrefixQueryConverter))]
	public sealed partial class MatchBoolPrefixQuery : Query, IQueryVariant
	{
		public MatchBoolPrefixQuery(Field field)
		{
			if (field is null)
				throw new ArgumentNullException(nameof(field));
			Field = field;
		}

		public string? QueryName { get; set; }

		public string? Analyzer { get; set; }

		public float? Boost { get; set; }

		public Elastic.Clients.Elasticsearch.Fuzziness? Fuzziness { get; set; }

		public string? FuzzyRewrite { get; set; }

		public bool? FuzzyTranspositions { get; set; }

		public int? MaxExpansions { get; set; }

		public Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatch { get; set; }

		public Elastic.Clients.Elasticsearch.QueryDsl.Operator? Operator { get; set; }

		public int? PrefixLength { get; set; }

		public string Query { get; set; }

		public Elastic.Clients.Elasticsearch.Field Field { get; set; }
	}

	public sealed partial class MatchBoolPrefixQueryDescriptor<TDocument> : SerializableDescriptorBase<MatchBoolPrefixQueryDescriptor<TDocument>>
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

		private string? QueryNameValue { get; set; }

		private string? AnalyzerValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fuzziness? FuzzinessValue { get; set; }

		private string? FuzzyRewriteValue { get; set; }

		private bool? FuzzyTranspositionsValue { get; set; }

		private int? MaxExpansionsValue { get; set; }

		private Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.Operator? OperatorValue { get; set; }

		private int? PrefixLengthValue { get; set; }

		private string QueryValue { get; set; }

		public MatchBoolPrefixQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

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

		public MatchBoolPrefixQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
		{
			FieldValue = field;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> Fuzziness(Elastic.Clients.Elasticsearch.Fuzziness? fuzziness)
		{
			FuzzinessValue = fuzziness;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> FuzzyRewrite(string? fuzzyRewrite)
		{
			FuzzyRewriteValue = fuzzyRewrite;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> FuzzyTranspositions(bool? fuzzyTranspositions = true)
		{
			FuzzyTranspositionsValue = fuzzyTranspositions;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> MaxExpansions(int? maxExpansions)
		{
			MaxExpansionsValue = maxExpansions;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? minimumShouldMatch)
		{
			MinimumShouldMatchValue = minimumShouldMatch;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> Operator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? op)
		{
			OperatorValue = op;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> PrefixLength(int? prefixLength)
		{
			PrefixLengthValue = prefixLength;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor<TDocument> Query(string query)
		{
			QueryValue = query;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (FieldValue is null)
				throw new JsonException("Unable to serialize field name query descriptor with a null field. Ensure you use a suitable descriptor constructor or call the Field method, passing a non-null value for the field argument.");
			writer.WriteStartObject();
			writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

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
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}

	public sealed partial class MatchBoolPrefixQueryDescriptor : SerializableDescriptorBase<MatchBoolPrefixQueryDescriptor>
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

		private string? QueryNameValue { get; set; }

		private string? AnalyzerValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

		private Elastic.Clients.Elasticsearch.Fuzziness? FuzzinessValue { get; set; }

		private string? FuzzyRewriteValue { get; set; }

		private bool? FuzzyTranspositionsValue { get; set; }

		private int? MaxExpansionsValue { get; set; }

		private Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.Operator? OperatorValue { get; set; }

		private int? PrefixLengthValue { get; set; }

		private string QueryValue { get; set; }

		public MatchBoolPrefixQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

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

		public MatchBoolPrefixQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
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

		public MatchBoolPrefixQueryDescriptor Fuzziness(Elastic.Clients.Elasticsearch.Fuzziness? fuzziness)
		{
			FuzzinessValue = fuzziness;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor FuzzyRewrite(string? fuzzyRewrite)
		{
			FuzzyRewriteValue = fuzzyRewrite;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor FuzzyTranspositions(bool? fuzzyTranspositions = true)
		{
			FuzzyTranspositionsValue = fuzzyTranspositions;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor MaxExpansions(int? maxExpansions)
		{
			MaxExpansionsValue = maxExpansions;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? minimumShouldMatch)
		{
			MinimumShouldMatchValue = minimumShouldMatch;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor Operator(Elastic.Clients.Elasticsearch.QueryDsl.Operator? op)
		{
			OperatorValue = op;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor PrefixLength(int? prefixLength)
		{
			PrefixLengthValue = prefixLength;
			return Self;
		}

		public MatchBoolPrefixQueryDescriptor Query(string query)
		{
			QueryValue = query;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (FieldValue is null)
				throw new JsonException("Unable to serialize field name query descriptor with a null field. Ensure you use a suitable descriptor constructor or call the Field method, passing a non-null value for the field argument.");
			writer.WriteStartObject();
			writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(QueryNameValue))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(QueryNameValue);
			}

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
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}