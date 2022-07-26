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
	internal sealed class MatchPhraseQueryConverter : JsonConverter<MatchPhraseQuery>
	{
		public override MatchPhraseQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new MatchPhraseQuery();
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

					if (property == "query")
					{
						variant.Query = JsonSerializer.Deserialize<string>(ref reader, options);
						continue;
					}

					if (property == "slop")
					{
						variant.Slop = JsonSerializer.Deserialize<int?>(ref reader, options);
						continue;
					}

					if (property == "zero_terms_query")
					{
						variant.ZeroTermsQuery = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery?>(ref reader, options);
						continue;
					}

					if (property == "field")
					{
						variant.Field = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						continue;
					}
				}
			}

			reader.Read();
			return variant;
		}

		public override void Write(Utf8JsonWriter writer, MatchPhraseQuery value, JsonSerializerOptions options)
		{
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

			writer.WritePropertyName("query");
			writer.WriteStringValue(value.Query);
			if (value.Slop.HasValue)
			{
				writer.WritePropertyName("slop");
				writer.WriteNumberValue(value.Slop.Value);
			}

			if (value.ZeroTermsQuery is not null)
			{
				writer.WritePropertyName("zero_terms_query");
				JsonSerializer.Serialize(writer, value.ZeroTermsQuery, options);
			}

			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(MatchPhraseQueryConverter))]
	public sealed partial class MatchPhraseQuery : IQueryVariant
	{
		public string? QueryName { get; set; }

		public string? Analyzer { get; set; }

		public float? Boost { get; set; }

		public string Query { get; set; }

		public int? Slop { get; set; }

		public Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? ZeroTermsQuery { get; set; }

		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }
	}

	public sealed partial class MatchPhraseQueryDescriptor<TDocument> : SerializableDescriptorBase<MatchPhraseQueryDescriptor<TDocument>>
	{
		internal MatchPhraseQueryDescriptor(Action<MatchPhraseQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
		public MatchPhraseQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private string? AnalyzerValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private string QueryValue { get; set; }

		private int? SlopValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? ZeroTermsQueryValue { get; set; }

		public MatchPhraseQueryDescriptor<TDocument> QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public MatchPhraseQueryDescriptor<TDocument> Analyzer(string? analyzer)
		{
			AnalyzerValue = analyzer;
			return Self;
		}

		public MatchPhraseQueryDescriptor<TDocument> Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public MatchPhraseQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public MatchPhraseQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public MatchPhraseQueryDescriptor<TDocument> Query(string query)
		{
			QueryValue = query;
			return Self;
		}

		public MatchPhraseQueryDescriptor<TDocument> Slop(int? slop)
		{
			SlopValue = slop;
			return Self;
		}

		public MatchPhraseQueryDescriptor<TDocument> ZeroTermsQuery(Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? zeroTermsQuery)
		{
			ZeroTermsQueryValue = zeroTermsQuery;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
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

			writer.WritePropertyName("query");
			writer.WriteStringValue(QueryValue);
			if (SlopValue.HasValue)
			{
				writer.WritePropertyName("slop");
				writer.WriteNumberValue(SlopValue.Value);
			}

			if (ZeroTermsQueryValue is not null)
			{
				writer.WritePropertyName("zero_terms_query");
				JsonSerializer.Serialize(writer, ZeroTermsQueryValue, options);
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}

	public sealed partial class MatchPhraseQueryDescriptor : SerializableDescriptorBase<MatchPhraseQueryDescriptor>
	{
		internal MatchPhraseQueryDescriptor(Action<MatchPhraseQueryDescriptor> configure) => configure.Invoke(this);
		public MatchPhraseQueryDescriptor() : base()
		{
		}

		private string? QueryNameValue { get; set; }

		private string? AnalyzerValue { get; set; }

		private float? BoostValue { get; set; }

		private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

		private string QueryValue { get; set; }

		private int? SlopValue { get; set; }

		private Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? ZeroTermsQueryValue { get; set; }

		public MatchPhraseQueryDescriptor QueryName(string? queryName)
		{
			QueryNameValue = queryName;
			return Self;
		}

		public MatchPhraseQueryDescriptor Analyzer(string? analyzer)
		{
			AnalyzerValue = analyzer;
			return Self;
		}

		public MatchPhraseQueryDescriptor Boost(float? boost)
		{
			BoostValue = boost;
			return Self;
		}

		public MatchPhraseQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
		{
			FieldValue = field;
			return Self;
		}

		public MatchPhraseQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
		{
			FieldValue = field;
			return Self;
		}

		public MatchPhraseQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
		{
			FieldValue = field;
			return Self;
		}

		public MatchPhraseQueryDescriptor Query(string query)
		{
			QueryValue = query;
			return Self;
		}

		public MatchPhraseQueryDescriptor Slop(int? slop)
		{
			SlopValue = slop;
			return Self;
		}

		public MatchPhraseQueryDescriptor ZeroTermsQuery(Elastic.Clients.Elasticsearch.QueryDsl.ZeroTermsQuery? zeroTermsQuery)
		{
			ZeroTermsQueryValue = zeroTermsQuery;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
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

			writer.WritePropertyName("query");
			writer.WriteStringValue(QueryValue);
			if (SlopValue.HasValue)
			{
				writer.WritePropertyName("slop");
				writer.WriteNumberValue(SlopValue.Value);
			}

			if (ZeroTermsQueryValue is not null)
			{
				writer.WritePropertyName("zero_terms_query");
				JsonSerializer.Serialize(writer, ZeroTermsQueryValue, options);
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}