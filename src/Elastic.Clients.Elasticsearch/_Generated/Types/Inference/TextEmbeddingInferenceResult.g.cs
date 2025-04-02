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

namespace Elastic.Clients.Elasticsearch.Inference;

internal sealed partial class TextEmbeddingInferenceResultConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingInferenceResult>
{
	private static readonly System.Text.Json.JsonEncodedText VariantTextEmbedding = System.Text.Json.JsonEncodedText.Encode("text_embedding");
	private static readonly System.Text.Json.JsonEncodedText VariantTextEmbeddingBits = System.Text.Json.JsonEncodedText.Encode("text_embedding_bits");
	private static readonly System.Text.Json.JsonEncodedText VariantTextEmbeddingBytes = System.Text.Json.JsonEncodedText.Encode("text_embedding_bytes");

	public override Elastic.Clients.Elasticsearch.Inference.TextEmbeddingInferenceResult Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		var variantType = string.Empty;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(VariantTextEmbedding))
			{
				variantType = VariantTextEmbedding.Value;
				reader.Read();
				variant = reader.ReadValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult>>(options, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult>(o, null)!);
				continue;
			}

			if (reader.ValueTextEquals(VariantTextEmbeddingBits))
			{
				variantType = VariantTextEmbeddingBits.Value;
				reader.Read();
				variant = reader.ReadValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>>(options, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>(o, null)!);
				continue;
			}

			if (reader.ValueTextEquals(VariantTextEmbeddingBytes))
			{
				variantType = VariantTextEmbeddingBytes.Value;
				reader.Read();
				variant = reader.ReadValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>>(options, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>(o, null)!);
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
		return new Elastic.Clients.Elasticsearch.Inference.TextEmbeddingInferenceResult(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.TextEmbeddingInferenceResult value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case "":
				break;
			case "text_embedding":
				writer.WriteProperty(options, value.VariantType, (System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult>)value.Variant, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult>(o, v, null));
				break;
			case "text_embedding_bits":
				writer.WriteProperty(options, value.VariantType, (System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>)value.Variant, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>(o, v, null));
				break;
			case "text_embedding_bytes":
				writer.WriteProperty(options, value.VariantType, (System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>)value.Variant, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>(o, v, null));
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.Inference.TextEmbeddingInferenceResult)}'.");
		}

		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.TextEmbeddingInferenceResultConverter))]
public sealed partial class TextEmbeddingInferenceResult
{
	public string VariantType { get; internal set; } = string.Empty;
	public object? Variant { get; internal set; }
#if NET7_0_OR_GREATER
	public TextEmbeddingInferenceResult()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public TextEmbeddingInferenceResult()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TextEmbeddingInferenceResult(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult>? TextEmbedding { get => GetVariant<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult>>("text_embedding"); set => SetVariant("text_embedding", value); }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>? TextEmbeddingBits { get => GetVariant<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>>("text_embedding_bits"); set => SetVariant("text_embedding_bits", value); }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>? TextEmbeddingBytes { get => GetVariant<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Inference.TextEmbeddingByteResult>>("text_embedding_bytes"); set => SetVariant("text_embedding_bytes", value); }

	public static implicit operator Elastic.Clients.Elasticsearch.Inference.TextEmbeddingInferenceResult(Elastic.Clients.Elasticsearch.Inference.TextEmbeddingResult[] value) => new Elastic.Clients.Elasticsearch.Inference.TextEmbeddingInferenceResult { TextEmbedding = value };

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