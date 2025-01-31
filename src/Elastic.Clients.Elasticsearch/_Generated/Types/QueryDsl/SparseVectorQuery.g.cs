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
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

[JsonConverter(typeof(SparseVectorQueryConverter))]
public sealed partial class SparseVectorQuery
{
	internal SparseVectorQuery(string variantName, object variant)
	{
		if (variantName is null)
			throw new ArgumentNullException(nameof(variantName));
		if (variant is null)
			throw new ArgumentNullException(nameof(variant));
		if (string.IsNullOrWhiteSpace(variantName))
			throw new ArgumentException("Variant name must not be empty or whitespace.");
		VariantType = variantName;
		Variant = variant;
	}

	internal SparseVectorQuery()
	{
	}

	public object Variant { get; internal set; }
	public string VariantType { get; internal set; }

	public static SparseVectorQuery InferenceId(Elastic.Clients.Elasticsearch.Id id) => new SparseVectorQuery("inference_id", id);

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }

	/// <summary>
	/// <para>
	/// The name of the field that contains the token-weight pairs to be searched against.
	/// This field must be a mapped sparse_vector field.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Whether to perform pruning, omitting the non-significant tokens from the query to improve query performance.
	/// If prune is true but the pruning_config is not specified, pruning will occur but default values will be used.
	/// Default: false
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prune")]
	public bool? Prune { get; set; }

	/// <summary>
	/// <para>
	/// Optional pruning configuration.
	/// If enabled, this will omit non-significant tokens from the query in order to improve query performance.
	/// This is only used if prune is set to true.
	/// If prune is set to true but pruning_config is not specified, default values will be used.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pruning_config")]
	public Elastic.Clients.Elasticsearch.QueryDsl.TokenPruningConfig? PruningConfig { get; set; }

	/// <summary>
	/// <para>
	/// The query text you want to use for search.
	/// If inference_id is specified, query must also be specified.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public string? Query { get; set; }
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

internal sealed partial class SparseVectorQueryConverter : System.Text.Json.Serialization.JsonConverter<SparseVectorQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropPrune = System.Text.Json.JsonEncodedText.Encode("prune");
	private static readonly System.Text.Json.JsonEncodedText PropPruningConfig = System.Text.Json.JsonEncodedText.Encode("pruning_config");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText VariantInferenceId = System.Text.Json.JsonEncodedText.Encode("inference_id");

	public override SparseVectorQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<bool?> propPrune = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.TokenPruningConfig?> propPruningConfig = default;
		LocalJsonValue<string?> propQuery = default;
		LocalJsonValue<string?> propQueryName = default;
		var variantType = string.Empty;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryRead(ref reader, options, PropBoost))
			{
				continue;
			}

			if (propField.TryRead(ref reader, options, PropField))
			{
				continue;
			}

			if (propPrune.TryRead(ref reader, options, PropPrune))
			{
				continue;
			}

			if (propPruningConfig.TryRead(ref reader, options, PropPruningConfig))
			{
				continue;
			}

			if (propQuery.TryRead(ref reader, options, PropQuery))
			{
				continue;
			}

			if (propQueryName.TryRead(ref reader, options, PropQueryName))
			{
				continue;
			}

			if (reader.ValueTextEquals(VariantInferenceId))
			{
				variantType = VariantInferenceId.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Id?>(options);
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new SparseVectorQuery
		{
			VariantType = variantType,
			Variant = variant,
			Boost = propBoost.Value
	,
			Field = propField.Value
	,
			Prune = propPrune.Value
	,
			PruningConfig = propPruningConfig.Value
	,
			Query = propQuery.Value
	,
			QueryName = propQueryName.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, SparseVectorQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case "":
				break;
			case "inference_id":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Id?)value.Variant);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(SparseVectorQuery)}'.");
		}

		writer.WriteProperty(options, PropBoost, value.Boost);
		writer.WriteProperty(options, PropField, value.Field);
		writer.WriteProperty(options, PropPrune, value.Prune);
		writer.WriteProperty(options, PropPruningConfig, value.PruningConfig);
		writer.WriteProperty(options, PropQuery, value.Query);
		writer.WriteProperty(options, PropQueryName, value.QueryName);
		writer.WriteEndObject();
	}
}

public sealed partial class SparseVectorQueryDescriptor<TDocument> : SerializableDescriptor<SparseVectorQueryDescriptor<TDocument>>
{
	internal SparseVectorQueryDescriptor(Action<SparseVectorQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SparseVectorQueryDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private SparseVectorQueryDescriptor<TDocument> Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private SparseVectorQueryDescriptor<TDocument> Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private bool? PruneValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.TokenPruningConfig? PruningConfigValue { get; set; }
	private string? QueryValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field that contains the token-weight pairs to be searched against.
	/// This field must be a mapped sparse_vector field.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field that contains the token-weight pairs to be searched against.
	/// This field must be a mapped sparse_vector field.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field that contains the token-weight pairs to be searched against.
	/// This field must be a mapped sparse_vector field.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Whether to perform pruning, omitting the non-significant tokens from the query to improve query performance.
	/// If prune is true but the pruning_config is not specified, pruning will occur but default values will be used.
	/// Default: false
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor<TDocument> Prune(bool? prune = true)
	{
		PruneValue = prune;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Optional pruning configuration.
	/// If enabled, this will omit non-significant tokens from the query in order to improve query performance.
	/// This is only used if prune is set to true.
	/// If prune is set to true but pruning_config is not specified, default values will be used.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor<TDocument> PruningConfig(Elastic.Clients.Elasticsearch.QueryDsl.TokenPruningConfig? pruningConfig)
	{
		PruningConfigValue = pruningConfig;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The query text you want to use for search.
	/// If inference_id is specified, query must also be specified.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor<TDocument> Query(string? query)
	{
		QueryValue = query;
		return Self;
	}

	public SparseVectorQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public SparseVectorQueryDescriptor<TDocument> InferenceId(Elastic.Clients.Elasticsearch.Id id) => Set(id, "inference_id");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (PruneValue.HasValue)
		{
			writer.WritePropertyName("prune");
			writer.WriteBooleanValue(PruneValue.Value);
		}

		if (PruningConfigValue is not null)
		{
			writer.WritePropertyName("pruning_config");
			JsonSerializer.Serialize(writer, PruningConfigValue, options);
		}

		if (!string.IsNullOrEmpty(QueryValue))
		{
			writer.WritePropertyName("query");
			writer.WriteStringValue(QueryValue);
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

public sealed partial class SparseVectorQueryDescriptor : SerializableDescriptor<SparseVectorQueryDescriptor>
{
	internal SparseVectorQueryDescriptor(Action<SparseVectorQueryDescriptor> configure) => configure.Invoke(this);

	public SparseVectorQueryDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private SparseVectorQueryDescriptor Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private SparseVectorQueryDescriptor Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private bool? PruneValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.TokenPruningConfig? PruningConfigValue { get; set; }
	private string? QueryValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field that contains the token-weight pairs to be searched against.
	/// This field must be a mapped sparse_vector field.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field that contains the token-weight pairs to be searched against.
	/// This field must be a mapped sparse_vector field.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field that contains the token-weight pairs to be searched against.
	/// This field must be a mapped sparse_vector field.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Whether to perform pruning, omitting the non-significant tokens from the query to improve query performance.
	/// If prune is true but the pruning_config is not specified, pruning will occur but default values will be used.
	/// Default: false
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor Prune(bool? prune = true)
	{
		PruneValue = prune;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Optional pruning configuration.
	/// If enabled, this will omit non-significant tokens from the query in order to improve query performance.
	/// This is only used if prune is set to true.
	/// If prune is set to true but pruning_config is not specified, default values will be used.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor PruningConfig(Elastic.Clients.Elasticsearch.QueryDsl.TokenPruningConfig? pruningConfig)
	{
		PruningConfigValue = pruningConfig;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The query text you want to use for search.
	/// If inference_id is specified, query must also be specified.
	/// </para>
	/// </summary>
	public SparseVectorQueryDescriptor Query(string? query)
	{
		QueryValue = query;
		return Self;
	}

	public SparseVectorQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public SparseVectorQueryDescriptor InferenceId(Elastic.Clients.Elasticsearch.Id id) => Set(id, "inference_id");

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (FieldValue is not null)
		{
			writer.WritePropertyName("field");
			JsonSerializer.Serialize(writer, FieldValue, options);
		}

		if (PruneValue.HasValue)
		{
			writer.WritePropertyName("prune");
			writer.WriteBooleanValue(PruneValue.Value);
		}

		if (PruningConfigValue is not null)
		{
			writer.WritePropertyName("pruning_config");
			JsonSerializer.Serialize(writer, PruningConfigValue, options);
		}

		if (!string.IsNullOrEmpty(QueryValue))
		{
			writer.WritePropertyName("query");
			writer.WriteStringValue(QueryValue);
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