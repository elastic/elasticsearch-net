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

[JsonConverter(typeof(PinnedQueryConverter))]
public sealed partial class PinnedQuery
{
	internal PinnedQuery(string variantName, object variant)
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

	public static PinnedQuery Docs(IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.PinnedDoc> pinnedDoc) => new PinnedQuery("docs", pinnedDoc);
	public static PinnedQuery Ids(IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Id> id) => new PinnedQuery("ids", id);

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }

	/// <summary>
	/// <para>Any choice of query used to rank documents which will be ranked below the "pinned" documents.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("organic")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query Organic { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }

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

internal sealed partial class PinnedQueryConverter : JsonConverter<PinnedQuery>
{
	public override PinnedQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException("Expected start token.");
		}

		object? variantValue = default;
		string? variantNameValue = default;
		float? boostValue = default;
		Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query organicValue = default;
		string? queryNameValue = default;
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
			if (propertyName == "boost")
			{
				boostValue = JsonSerializer.Deserialize<float?>(ref reader, options);
				continue;
			}

			if (propertyName == "organic")
			{
				organicValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query>(ref reader, options);
				continue;
			}

			if (propertyName == "_name")
			{
				queryNameValue = JsonSerializer.Deserialize<string?>(ref reader, options);
				continue;
			}

			if (propertyName == "docs")
			{
				variantValue = JsonSerializer.Deserialize<ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.PinnedDoc>?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "ids")
			{
				variantValue = JsonSerializer.Deserialize<ICollection<Elastic.Clients.Elasticsearch.Serverless.Id>?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			throw new JsonException($"Unknown property name '{propertyName}' received while deserializing the 'PinnedQuery' from the response.");
		}

		reader.Read();
		var result = new PinnedQuery(variantNameValue, variantValue);
		result.Boost = boostValue;
		result.Organic = organicValue;
		result.QueryName = queryNameValue;
		return result;
	}

	public override void Write(Utf8JsonWriter writer, PinnedQuery value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		if (value.Boost.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(value.Boost.Value);
		}

		if (value.Organic is not null)
		{
			writer.WritePropertyName("organic");
			JsonSerializer.Serialize(writer, value.Organic, options);
		}

		if (!string.IsNullOrEmpty(value.QueryName))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(value.QueryName);
		}

		if (value.VariantName is not null && value.Variant is not null)
		{
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "docs":
					JsonSerializer.Serialize<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.PinnedDoc>>(writer, (IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.PinnedDoc>)value.Variant, options);
					break;
				case "ids":
					JsonSerializer.Serialize<IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Id>>(writer, (IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Id>)value.Variant, options);
					break;
			}
		}

		writer.WriteEndObject();
	}
}

public sealed partial class PinnedQueryDescriptor<TDocument> : SerializableDescriptor<PinnedQueryDescriptor<TDocument>>
{
	internal PinnedQueryDescriptor(Action<PinnedQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PinnedQueryDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private PinnedQueryDescriptor<TDocument> Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private PinnedQueryDescriptor<TDocument> Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query OrganicValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public PinnedQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>Any choice of query used to rank documents which will be ranked below the "pinned" documents.</para>
	/// </summary>
	public PinnedQueryDescriptor<TDocument> Organic(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query organic)
	{
		OrganicValue = organic;
		return Self;
	}

	public PinnedQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public PinnedQueryDescriptor<TDocument> Docs(IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.PinnedDoc> pinnedDoc) => Set(pinnedDoc, "docs");
	public PinnedQueryDescriptor<TDocument> Docs(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.PinnedDocDescriptor> configure) => Set(configure, "docs");
	public PinnedQueryDescriptor<TDocument> Ids(IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Id> id) => Set(id, "ids");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (OrganicValue is not null)
		{
			writer.WritePropertyName("organic");
			JsonSerializer.Serialize(writer, OrganicValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

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

public sealed partial class PinnedQueryDescriptor : SerializableDescriptor<PinnedQueryDescriptor>
{
	internal PinnedQueryDescriptor(Action<PinnedQueryDescriptor> configure) => configure.Invoke(this);

	public PinnedQueryDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private PinnedQueryDescriptor Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private PinnedQueryDescriptor Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query OrganicValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public PinnedQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>Any choice of query used to rank documents which will be ranked below the "pinned" documents.</para>
	/// </summary>
	public PinnedQueryDescriptor Organic(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query organic)
	{
		OrganicValue = organic;
		return Self;
	}

	public PinnedQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public PinnedQueryDescriptor Docs(IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.PinnedDoc> pinnedDoc) => Set(pinnedDoc, "docs");
	public PinnedQueryDescriptor Docs(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.PinnedDocDescriptor> configure) => Set(configure, "docs");
	public PinnedQueryDescriptor Ids(IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Id> id) => Set(id, "ids");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (OrganicValue is not null)
		{
			writer.WritePropertyName("organic");
			JsonSerializer.Serialize(writer, OrganicValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

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