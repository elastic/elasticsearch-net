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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Ingest;

internal sealed partial class InferenceConfigConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Ingest.InferenceConfig>
{
	private static readonly System.Text.Json.JsonEncodedText VariantClassification = System.Text.Json.JsonEncodedText.Encode("classification");
	private static readonly System.Text.Json.JsonEncodedText VariantRegression = System.Text.Json.JsonEncodedText.Encode("regression");

	public override Elastic.Clients.Elasticsearch.Ingest.InferenceConfig Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantRegression))
			{
				variantType = VariantRegression.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression>(options, null);
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Ingest.InferenceConfig value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case "":
				break;
			case "classification":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification)value.Variant, null, null);
				break;
			case "regression":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.Ingest.InferenceConfig)}'.");
		}

		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigConverter))]
public sealed partial class InferenceConfig
{
	public string VariantType { get; internal set; } = string.Empty;
	public object? Variant { get; internal set; }
#if NET7_0_OR_GREATER
	public InferenceConfig()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public InferenceConfig()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal InferenceConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Classification configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification? Classification { get => GetVariant<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification>("classification"); set => SetVariant("classification", value); }

	/// <summary>
	/// <para>
	/// Regression configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression? Regression { get => GetVariant<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression>("regression"); set => SetVariant("regression", value); }

	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification value) => new Elastic.Clients.Elasticsearch.Ingest.InferenceConfig { Classification = value };
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression value) => new Elastic.Clients.Elasticsearch.Ingest.InferenceConfig { Regression = value };

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private T? GetVariant<T>(string type)
	{
		if (string.Equals(VariantType, type, System.StringComparison.Ordinal) && Variant is T result)
		{
			return result;
		}

		return default;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private void SetVariant<T>(string type, T? value)
	{
		VariantType = type;
		Variant = value;
	}
}

public readonly partial struct InferenceConfigDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Ingest.InferenceConfig Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InferenceConfigDescriptor(Elastic.Clients.Elasticsearch.Ingest.InferenceConfig instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InferenceConfigDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Ingest.InferenceConfig instance) => new Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Classification configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument> Classification(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification? value)
	{
		Instance.Classification = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Classification configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument> Classification()
	{
		Instance.Classification = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Classification configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument> Classification(System.Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor<TDocument>>? action)
	{
		Instance.Classification = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Regression configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument> Regression(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression? value)
	{
		Instance.Regression = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Regression configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument> Regression()
	{
		Instance.Regression = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Regression configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument> Regression(System.Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor<TDocument>>? action)
	{
		Instance.Regression = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor<TDocument>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.InferenceConfig Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct InferenceConfigDescriptor
{
	internal Elastic.Clients.Elasticsearch.Ingest.InferenceConfig Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InferenceConfigDescriptor(Elastic.Clients.Elasticsearch.Ingest.InferenceConfig instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public InferenceConfigDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor(Elastic.Clients.Elasticsearch.Ingest.InferenceConfig instance) => new Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Classification configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor Classification(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassification? value)
	{
		Instance.Classification = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Classification configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor Classification()
	{
		Instance.Classification = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Classification configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor Classification(System.Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor>? action)
	{
		Instance.Classification = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Classification configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor Classification<T>(System.Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor<T>>? action)
	{
		Instance.Classification = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigClassificationDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Regression configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor Regression(Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegression? value)
	{
		Instance.Regression = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Regression configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor Regression()
	{
		Instance.Regression = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Regression configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor Regression(System.Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor>? action)
	{
		Instance.Regression = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Regression configuration for inference.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor Regression<T>(System.Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor<T>>? action)
	{
		Instance.Regression = Elastic.Clients.Elasticsearch.Ingest.InferenceConfigRegressionDescriptor<T>.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Ingest.InferenceConfig Build(System.Action<Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Ingest.InferenceConfigDescriptor(new Elastic.Clients.Elasticsearch.Ingest.InferenceConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}