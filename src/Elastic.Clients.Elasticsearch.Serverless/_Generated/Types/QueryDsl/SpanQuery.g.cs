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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;

[JsonConverter(typeof(SpanQueryConverter))]
public sealed partial class SpanQuery
{
	internal SpanQuery(string variantName, object variant)
	{
		if (variantName is null)
			throw new ArgumentNullException(nameof(variantName));
		if (variant is null)
			throw new ArgumentNullException(nameof(variant));
		if (string.IsNullOrWhiteSpace(variantName))
			throw new ArgumentException("Variant name must not be empty or whitespace.");
		VariantName = variantName;
		Variant = variant;
	}

	internal object Variant { get; }
	internal string VariantName { get; }

	public static SpanQuery SpanContaining(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanContainingQuery spanContainingQuery) => new SpanQuery("span_containing", spanContainingQuery);
	public static SpanQuery SpanFieldMasking(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFieldMaskingQuery spanFieldMaskingQuery) => new SpanQuery("span_field_masking", spanFieldMaskingQuery);
	public static SpanQuery SpanFirst(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFirstQuery spanFirstQuery) => new SpanQuery("span_first", spanFirstQuery);
	public static SpanQuery SpanGap(KeyValuePair<Elastic.Clients.Elasticsearch.Serverless.Field, int> integer) => new SpanQuery("span_gap", integer);
	public static SpanQuery SpanMulti(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanMultiTermQuery spanMultiTermQuery) => new SpanQuery("span_multi", spanMultiTermQuery);
	public static SpanQuery SpanNear(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNearQuery spanNearQuery) => new SpanQuery("span_near", spanNearQuery);
	public static SpanQuery SpanNot(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNotQuery spanNotQuery) => new SpanQuery("span_not", spanNotQuery);
	public static SpanQuery SpanOr(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanOrQuery spanOrQuery) => new SpanQuery("span_or", spanOrQuery);
	public static SpanQuery SpanTerm(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanTermQuery spanTermQuery) => new SpanQuery("span_term", spanTermQuery);
	public static SpanQuery SpanWithin(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanWithinQuery spanWithinQuery) => new SpanQuery("span_within", spanWithinQuery);

	public bool TryGet<T>([NotNullWhen(true)] out T? result) where T : class
	{
		result = default;
		if (Variant is T variant)
		{
			result = variant;
			return true;
		}

		return false;
	}
}

internal sealed partial class SpanQueryConverter : JsonConverter<SpanQuery>
{
	public override SpanQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException("Expected start token.");
		}

		object? variantValue = default;
		string? variantNameValue = default;
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException("Expected a property name token.");
			}

			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				throw new JsonException("Expected a property name token representing the name of an Elasticsearch field.");
			}

			var propertyName = reader.GetString();
			reader.Read();
			if (propertyName == "span_containing")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanContainingQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_field_masking")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFieldMaskingQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_first")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFirstQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_gap")
			{
				variantValue = JsonSerializer.Deserialize<KeyValuePair<Elastic.Clients.Elasticsearch.Serverless.Field, int>?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_multi")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanMultiTermQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_near")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNearQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_not")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNotQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_or")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanOrQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_term")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanTermQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "span_within")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanWithinQuery?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			throw new JsonException($"Unknown property name '{propertyName}' received while deserializing the 'SpanQuery' from the response.");
		}

		var result = new SpanQuery(variantNameValue, variantValue);
		return result;
	}

	public override void Write(Utf8JsonWriter writer, SpanQuery value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		if (value.VariantName is not null && value.Variant is not null)
		{
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "span_containing":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanContainingQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanContainingQuery)value.Variant, options);
					break;
				case "span_field_masking":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFieldMaskingQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFieldMaskingQuery)value.Variant, options);
					break;
				case "span_first":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFirstQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFirstQuery)value.Variant, options);
					break;
				case "span_gap":
					JsonSerializer.Serialize<KeyValuePair<Elastic.Clients.Elasticsearch.Serverless.Field, int>>(writer, (KeyValuePair<Elastic.Clients.Elasticsearch.Serverless.Field, int>)value.Variant, options);
					break;
				case "span_multi":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanMultiTermQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanMultiTermQuery)value.Variant, options);
					break;
				case "span_near":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNearQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNearQuery)value.Variant, options);
					break;
				case "span_not":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNotQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNotQuery)value.Variant, options);
					break;
				case "span_or":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanOrQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanOrQuery)value.Variant, options);
					break;
				case "span_term":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanTermQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanTermQuery)value.Variant, options);
					break;
				case "span_within":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanWithinQuery>(writer, (Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanWithinQuery)value.Variant, options);
					break;
			}
		}

		writer.WriteEndObject();
	}
}

public sealed partial class SpanQueryDescriptor<TDocument> : SerializableDescriptor<SpanQueryDescriptor<TDocument>>
{
	internal SpanQueryDescriptor(Action<SpanQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SpanQueryDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private SpanQueryDescriptor<TDocument> Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private SpanQueryDescriptor<TDocument> Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public SpanQueryDescriptor<TDocument> SpanContaining(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanContainingQuery spanContainingQuery) => Set(spanContainingQuery, "span_containing");
	public SpanQueryDescriptor<TDocument> SpanContaining(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanContainingQueryDescriptor<TDocument>> configure) => Set(configure, "span_containing");
	public SpanQueryDescriptor<TDocument> SpanFieldMasking(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFieldMaskingQuery spanFieldMaskingQuery) => Set(spanFieldMaskingQuery, "span_field_masking");
	public SpanQueryDescriptor<TDocument> SpanFieldMasking(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFieldMaskingQueryDescriptor<TDocument>> configure) => Set(configure, "span_field_masking");
	public SpanQueryDescriptor<TDocument> SpanFirst(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFirstQuery spanFirstQuery) => Set(spanFirstQuery, "span_first");
	public SpanQueryDescriptor<TDocument> SpanFirst(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFirstQueryDescriptor<TDocument>> configure) => Set(configure, "span_first");
	public SpanQueryDescriptor<TDocument> SpanGap(KeyValuePair<Elastic.Clients.Elasticsearch.Serverless.Field, int> integer) => Set(integer, "span_gap");
	public SpanQueryDescriptor<TDocument> SpanMulti(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanMultiTermQuery spanMultiTermQuery) => Set(spanMultiTermQuery, "span_multi");
	public SpanQueryDescriptor<TDocument> SpanMulti(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanMultiTermQueryDescriptor<TDocument>> configure) => Set(configure, "span_multi");
	public SpanQueryDescriptor<TDocument> SpanNear(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNearQuery spanNearQuery) => Set(spanNearQuery, "span_near");
	public SpanQueryDescriptor<TDocument> SpanNear(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNearQueryDescriptor<TDocument>> configure) => Set(configure, "span_near");
	public SpanQueryDescriptor<TDocument> SpanNot(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNotQuery spanNotQuery) => Set(spanNotQuery, "span_not");
	public SpanQueryDescriptor<TDocument> SpanNot(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNotQueryDescriptor<TDocument>> configure) => Set(configure, "span_not");
	public SpanQueryDescriptor<TDocument> SpanOr(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanOrQuery spanOrQuery) => Set(spanOrQuery, "span_or");
	public SpanQueryDescriptor<TDocument> SpanOr(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanOrQueryDescriptor<TDocument>> configure) => Set(configure, "span_or");
	public SpanQueryDescriptor<TDocument> SpanTerm(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanTermQuery spanTermQuery) => Set(spanTermQuery, "span_term");
	public SpanQueryDescriptor<TDocument> SpanTerm(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanTermQueryDescriptor<TDocument>> configure) => Set(configure, "span_term");
	public SpanQueryDescriptor<TDocument> SpanWithin(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanWithinQuery spanWithinQuery) => Set(spanWithinQuery, "span_within");
	public SpanQueryDescriptor<TDocument> SpanWithin(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanWithinQueryDescriptor<TDocument>> configure) => Set(configure, "span_within");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ContainedVariantName))
		{
			writer.WritePropertyName(ContainedVariantName);
			if (Variant is not null)
			{
				JsonSerializer.Serialize(writer, Variant, Variant.GetType(), options);
				writer.WriteEndObject();
				return;
			}

			JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class SpanQueryDescriptor : SerializableDescriptor<SpanQueryDescriptor>
{
	internal SpanQueryDescriptor(Action<SpanQueryDescriptor> configure) => configure.Invoke(this);

	public SpanQueryDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private SpanQueryDescriptor Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private SpanQueryDescriptor Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public SpanQueryDescriptor SpanContaining(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanContainingQuery spanContainingQuery) => Set(spanContainingQuery, "span_containing");
	public SpanQueryDescriptor SpanContaining<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanContainingQueryDescriptor> configure) => Set(configure, "span_containing");
	public SpanQueryDescriptor SpanFieldMasking(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFieldMaskingQuery spanFieldMaskingQuery) => Set(spanFieldMaskingQuery, "span_field_masking");
	public SpanQueryDescriptor SpanFieldMasking<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFieldMaskingQueryDescriptor> configure) => Set(configure, "span_field_masking");
	public SpanQueryDescriptor SpanFirst(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFirstQuery spanFirstQuery) => Set(spanFirstQuery, "span_first");
	public SpanQueryDescriptor SpanFirst<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanFirstQueryDescriptor> configure) => Set(configure, "span_first");
	public SpanQueryDescriptor SpanGap(KeyValuePair<Elastic.Clients.Elasticsearch.Serverless.Field, int> integer) => Set(integer, "span_gap");
	public SpanQueryDescriptor SpanMulti(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanMultiTermQuery spanMultiTermQuery) => Set(spanMultiTermQuery, "span_multi");
	public SpanQueryDescriptor SpanMulti<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanMultiTermQueryDescriptor> configure) => Set(configure, "span_multi");
	public SpanQueryDescriptor SpanNear(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNearQuery spanNearQuery) => Set(spanNearQuery, "span_near");
	public SpanQueryDescriptor SpanNear<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNearQueryDescriptor> configure) => Set(configure, "span_near");
	public SpanQueryDescriptor SpanNot(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNotQuery spanNotQuery) => Set(spanNotQuery, "span_not");
	public SpanQueryDescriptor SpanNot<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanNotQueryDescriptor> configure) => Set(configure, "span_not");
	public SpanQueryDescriptor SpanOr(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanOrQuery spanOrQuery) => Set(spanOrQuery, "span_or");
	public SpanQueryDescriptor SpanOr<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanOrQueryDescriptor> configure) => Set(configure, "span_or");
	public SpanQueryDescriptor SpanTerm(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanTermQuery spanTermQuery) => Set(spanTermQuery, "span_term");
	public SpanQueryDescriptor SpanTerm<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanTermQueryDescriptor> configure) => Set(configure, "span_term");
	public SpanQueryDescriptor SpanWithin(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanWithinQuery spanWithinQuery) => Set(spanWithinQuery, "span_within");
	public SpanQueryDescriptor SpanWithin<TDocument>(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanWithinQueryDescriptor> configure) => Set(configure, "span_within");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ContainedVariantName))
		{
			writer.WritePropertyName(ContainedVariantName);
			if (Variant is not null)
			{
				JsonSerializer.Serialize(writer, Variant, Variant.GetType(), options);
				writer.WriteEndObject();
				return;
			}

			JsonSerializer.Serialize(writer, Descriptor, Descriptor.GetType(), options);
		}

		writer.WriteEndObject();
	}
}