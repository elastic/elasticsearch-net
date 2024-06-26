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
		VariantName = variantName;
		Variant = variant;
	}

	internal object Variant { get; }
	internal string VariantName { get; }

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

internal sealed partial class PreprocessorConverter : JsonConverter<Preprocessor>
{
	public override Preprocessor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			if (propertyName == "frequency_encoding")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "one_hot_encoding")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			if (propertyName == "target_mean_encoding")
			{
				variantValue = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessor?>(ref reader, options);
				variantNameValue = propertyName;
				continue;
			}

			throw new JsonException($"Unknown property name '{propertyName}' received while deserializing the 'Preprocessor' from the response.");
		}

		var result = new Preprocessor(variantNameValue, variantValue);
		return result;
	}

	public override void Write(Utf8JsonWriter writer, Preprocessor value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		if (value.VariantName is not null && value.Variant is not null)
		{
			writer.WritePropertyName(value.VariantName);
			switch (value.VariantName)
			{
				case "frequency_encoding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor>(writer, (Elastic.Clients.Elasticsearch.MachineLearning.FrequencyEncodingPreprocessor)value.Variant, options);
					break;
				case "one_hot_encoding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor>(writer, (Elastic.Clients.Elasticsearch.MachineLearning.OneHotEncodingPreprocessor)value.Variant, options);
					break;
				case "target_mean_encoding":
					JsonSerializer.Serialize<Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessor>(writer, (Elastic.Clients.Elasticsearch.MachineLearning.TargetMeanEncodingPreprocessor)value.Variant, options);
					break;
			}
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