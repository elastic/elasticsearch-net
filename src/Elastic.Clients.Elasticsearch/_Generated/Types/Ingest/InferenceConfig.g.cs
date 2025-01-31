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

namespace Elastic.Clients.Elasticsearch.Ingest;

[JsonConverter(typeof(InferenceConfigConverter))]
public sealed partial class InferenceConfig
{
	internal InferenceConfig(string variantName, object variant)
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

	internal InferenceConfig()
	{
	}

	public object Variant { get; internal set; }
	public string VariantType { get; internal set; }

	public static InferenceConfig Classification(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification inferenceConfigClassification) => new InferenceConfig("classification", inferenceConfigClassification);
	public static InferenceConfig Regression(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression inferenceConfigRegression) => new InferenceConfig("regression", inferenceConfigRegression);

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

internal sealed partial class InferenceConfigConverter : System.Text.Json.Serialization.JsonConverter<InferenceConfig>
{
	private static readonly System.Text.Json.JsonEncodedText VariantClassification = System.Text.Json.JsonEncodedText.Encode("classification");
	private static readonly System.Text.Json.JsonEncodedText VariantRegression = System.Text.Json.JsonEncodedText.Encode("regression");

	public override InferenceConfig Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		var variantType = string.Empty;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(VariantClassification))
			{
				variantType = VariantClassification.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification?>(options);
				continue;
			}

			if (reader.ValueTextEquals(VariantRegression))
			{
				variantType = VariantRegression.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression?>(options);
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new InferenceConfig { VariantType = variantType, Variant = variant };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, InferenceConfig value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case "":
				break;
			case "classification":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification?)value.Variant);
				break;
			case "regression":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression?)value.Variant);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(InferenceConfig)}'.");
		}

		writer.WriteEndObject();
	}
}

public sealed partial class InferenceConfigDescriptor<TDocument> : SerializableDescriptor<InferenceConfigDescriptor<TDocument>>
{
	internal InferenceConfigDescriptor(Action<InferenceConfigDescriptor<TDocument>> configure) => configure.Invoke(this);

	public InferenceConfigDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private InferenceConfigDescriptor<TDocument> Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private InferenceConfigDescriptor<TDocument> Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public InferenceConfigDescriptor<TDocument> Classification(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification inferenceConfigClassification) => Set(inferenceConfigClassification, "classification");
	public InferenceConfigDescriptor<TDocument> Classification(Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor<TDocument>> configure) => Set(configure, "classification");
	public InferenceConfigDescriptor<TDocument> Regression(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression inferenceConfigRegression) => Set(inferenceConfigRegression, "regression");
	public InferenceConfigDescriptor<TDocument> Regression(Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor<TDocument>> configure) => Set(configure, "regression");

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

public sealed partial class InferenceConfigDescriptor : SerializableDescriptor<InferenceConfigDescriptor>
{
	internal InferenceConfigDescriptor(Action<InferenceConfigDescriptor> configure) => configure.Invoke(this);

	public InferenceConfigDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private InferenceConfigDescriptor Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private InferenceConfigDescriptor Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public InferenceConfigDescriptor Classification(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification inferenceConfigClassification) => Set(inferenceConfigClassification, "classification");
	public InferenceConfigDescriptor Classification<TDocument>(Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor> configure) => Set(configure, "classification");
	public InferenceConfigDescriptor Regression(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression inferenceConfigRegression) => Set(inferenceConfigRegression, "regression");
	public InferenceConfigDescriptor Regression<TDocument>(Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor> configure) => Set(configure, "regression");

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