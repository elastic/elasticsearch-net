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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

[JsonConverter(typeof(PreprocessorConverter))]
public sealed partial class Preprocessor
{
	internal Preprocessor(string variantName, object variant)
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

	internal Preprocessor()
	{
	}

	public object Variant { get; internal set; }
	public string VariantType { get; internal set; }

	public static Preprocessor FrequencyEncoding(Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor frequencyEncodingPreprocessor) => new Preprocessor("frequency_encoding", frequencyEncodingPreprocessor);
	public static Preprocessor OneHotEncoding(Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor oneHotEncodingPreprocessor) => new Preprocessor("one_hot_encoding", oneHotEncodingPreprocessor);
	public static Preprocessor TargetMeanEncoding(Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessor targetMeanEncodingPreprocessor) => new Preprocessor("target_mean_encoding", targetMeanEncodingPreprocessor);

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

internal sealed partial class PreprocessorConverter : System.Text.Json.Serialization.JsonConverter<Preprocessor>
{
	private static readonly System.Text.Json.JsonEncodedText VariantFrequencyEncoding = System.Text.Json.JsonEncodedText.Encode("frequency_encoding");
	private static readonly System.Text.Json.JsonEncodedText VariantOneHotEncoding = System.Text.Json.JsonEncodedText.Encode("one_hot_encoding");
	private static readonly System.Text.Json.JsonEncodedText VariantTargetMeanEncoding = System.Text.Json.JsonEncodedText.Encode("target_mean_encoding");

	public override Preprocessor Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		var variantType = string.Empty;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(VariantFrequencyEncoding))
			{
				variantType = VariantFrequencyEncoding.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor?>(options);
				continue;
			}

			if (reader.ValueTextEquals(VariantOneHotEncoding))
			{
				variantType = VariantOneHotEncoding.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor?>(options);
				continue;
			}

			if (reader.ValueTextEquals(VariantTargetMeanEncoding))
			{
				variantType = VariantTargetMeanEncoding.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessor?>(options);
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Preprocessor { VariantType = variantType, Variant = variant };
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Preprocessor value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case "":
				break;
			case "frequency_encoding":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor?)value.Variant);
				break;
			case "one_hot_encoding":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor?)value.Variant);
				break;
			case "target_mean_encoding":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessor?)value.Variant);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Preprocessor)}'.");
		}

		writer.WriteEndObject();
	}
}

public sealed partial class PreprocessorDescriptor<TDocument> : SerializableDescriptor<PreprocessorDescriptor<TDocument>>
{
	internal PreprocessorDescriptor(Action<PreprocessorDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PreprocessorDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private PreprocessorDescriptor<TDocument> Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private PreprocessorDescriptor<TDocument> Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public PreprocessorDescriptor<TDocument> FrequencyEncoding(Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor frequencyEncodingPreprocessor) => Set(frequencyEncodingPreprocessor, "frequency_encoding");
	public PreprocessorDescriptor<TDocument> FrequencyEncoding(Action<Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor> configure) => Set(configure, "frequency_encoding");
	public PreprocessorDescriptor<TDocument> OneHotEncoding(Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor oneHotEncodingPreprocessor) => Set(oneHotEncodingPreprocessor, "one_hot_encoding");
	public PreprocessorDescriptor<TDocument> OneHotEncoding(Action<Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor> configure) => Set(configure, "one_hot_encoding");
	public PreprocessorDescriptor<TDocument> TargetMeanEncoding(Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessor targetMeanEncodingPreprocessor) => Set(targetMeanEncodingPreprocessor, "target_mean_encoding");
	public PreprocessorDescriptor<TDocument> TargetMeanEncoding(Action<Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessorDescriptor> configure) => Set(configure, "target_mean_encoding");

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

public sealed partial class PreprocessorDescriptor : SerializableDescriptor<PreprocessorDescriptor>
{
	internal PreprocessorDescriptor(Action<PreprocessorDescriptor> configure) => configure.Invoke(this);

	public PreprocessorDescriptor() : base()
	{
	}

	private bool ContainsVariant { get; set; }
	private string ContainedVariantName { get; set; }
	private object Variant { get; set; }
	private Descriptor Descriptor { get; set; }

	private PreprocessorDescriptor Set<T>(Action<T> descriptorAction, string variantName) where T : Descriptor
	{
		ContainedVariantName = variantName;
		ContainsVariant = true;
		var descriptor = (T)Activator.CreateInstance(typeof(T), true);
		descriptorAction?.Invoke(descriptor);
		Descriptor = descriptor;
		return Self;
	}

	private PreprocessorDescriptor Set(object variant, string variantName)
	{
		Variant = variant;
		ContainedVariantName = variantName;
		ContainsVariant = true;
		return Self;
	}

	public PreprocessorDescriptor FrequencyEncoding(Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor frequencyEncodingPreprocessor) => Set(frequencyEncodingPreprocessor, "frequency_encoding");
	public PreprocessorDescriptor FrequencyEncoding(Action<Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessorDescriptor> configure) => Set(configure, "frequency_encoding");
	public PreprocessorDescriptor OneHotEncoding(Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor oneHotEncodingPreprocessor) => Set(oneHotEncodingPreprocessor, "one_hot_encoding");
	public PreprocessorDescriptor OneHotEncoding(Action<Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessorDescriptor> configure) => Set(configure, "one_hot_encoding");
	public PreprocessorDescriptor TargetMeanEncoding(Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessor targetMeanEncodingPreprocessor) => Set(targetMeanEncodingPreprocessor, "target_mean_encoding");
	public PreprocessorDescriptor TargetMeanEncoding(Action<Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessorDescriptor> configure) => Set(configure, "target_mean_encoding");

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