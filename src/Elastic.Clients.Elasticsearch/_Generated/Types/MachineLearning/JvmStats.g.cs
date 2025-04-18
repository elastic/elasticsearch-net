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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class JvmStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.JvmStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropHeapMax = System.Text.Json.JsonEncodedText.Encode("heap_max");
	private static readonly System.Text.Json.JsonEncodedText PropHeapMaxInBytes = System.Text.Json.JsonEncodedText.Encode("heap_max_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropJavaInference = System.Text.Json.JsonEncodedText.Encode("java_inference");
	private static readonly System.Text.Json.JsonEncodedText PropJavaInferenceInBytes = System.Text.Json.JsonEncodedText.Encode("java_inference_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropJavaInferenceMax = System.Text.Json.JsonEncodedText.Encode("java_inference_max");
	private static readonly System.Text.Json.JsonEncodedText PropJavaInferenceMaxInBytes = System.Text.Json.JsonEncodedText.Encode("java_inference_max_in_bytes");

	public override Elastic.Clients.Elasticsearch.MachineLearning.JvmStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propHeapMax = default;
		LocalJsonValue<int> propHeapMaxInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propJavaInference = default;
		LocalJsonValue<int> propJavaInferenceInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propJavaInferenceMax = default;
		LocalJsonValue<int> propJavaInferenceMaxInBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propHeapMax.TryReadProperty(ref reader, options, PropHeapMax, null))
			{
				continue;
			}

			if (propHeapMaxInBytes.TryReadProperty(ref reader, options, PropHeapMaxInBytes, null))
			{
				continue;
			}

			if (propJavaInference.TryReadProperty(ref reader, options, PropJavaInference, null))
			{
				continue;
			}

			if (propJavaInferenceInBytes.TryReadProperty(ref reader, options, PropJavaInferenceInBytes, null))
			{
				continue;
			}

			if (propJavaInferenceMax.TryReadProperty(ref reader, options, PropJavaInferenceMax, null))
			{
				continue;
			}

			if (propJavaInferenceMaxInBytes.TryReadProperty(ref reader, options, PropJavaInferenceMaxInBytes, null))
			{
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.JvmStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			HeapMax = propHeapMax.Value,
			HeapMaxInBytes = propHeapMaxInBytes.Value,
			JavaInference = propJavaInference.Value,
			JavaInferenceInBytes = propJavaInferenceInBytes.Value,
			JavaInferenceMax = propJavaInferenceMax.Value,
			JavaInferenceMaxInBytes = propJavaInferenceMaxInBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.JvmStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropHeapMax, value.HeapMax, null, null);
		writer.WriteProperty(options, PropHeapMaxInBytes, value.HeapMaxInBytes, null, null);
		writer.WriteProperty(options, PropJavaInference, value.JavaInference, null, null);
		writer.WriteProperty(options, PropJavaInferenceInBytes, value.JavaInferenceInBytes, null, null);
		writer.WriteProperty(options, PropJavaInferenceMax, value.JavaInferenceMax, null, null);
		writer.WriteProperty(options, PropJavaInferenceMaxInBytes, value.JavaInferenceMaxInBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.JvmStatsConverter))]
public sealed partial class JvmStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JvmStats(int heapMaxInBytes, int javaInferenceInBytes, int javaInferenceMaxInBytes)
	{
		HeapMaxInBytes = heapMaxInBytes;
		JavaInferenceInBytes = javaInferenceInBytes;
		JavaInferenceMaxInBytes = javaInferenceMaxInBytes;
	}
#if NET7_0_OR_GREATER
	public JvmStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public JvmStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal JvmStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Maximum amount of memory available for use by the heap.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? HeapMax { get; set; }

	/// <summary>
	/// <para>
	/// Maximum amount of memory, in bytes, available for use by the heap.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int HeapMaxInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Amount of Java heap currently being used for caching inference models.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? JavaInference { get; set; }

	/// <summary>
	/// <para>
	/// Amount of Java heap, in bytes, currently being used for caching inference models.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int JavaInferenceInBytes { get; set; }

	/// <summary>
	/// <para>
	/// Maximum amount of Java heap to be used for caching inference models.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? JavaInferenceMax { get; set; }

	/// <summary>
	/// <para>
	/// Maximum amount of Java heap, in bytes, to be used for caching inference models.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int JavaInferenceMaxInBytes { get; set; }
}