// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text.Json;
using Elastic.Transport;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.Serialization;
#else
namespace Elastic.Clients.Elasticsearch.Serialization;
#endif

/// <summary>
/// May be used by requests that need to serialize only part of their source rather than the request object itself.
/// </summary>
internal static class SourceSerialization
{
	public static void SerializeParams<T>(T toSerialize, Utf8JsonWriter writer, IElasticsearchClientSettings settings)
	{
		if (settings.Experimental.UseSourceSerializerForScriptParameters)
		{
			Serialize(toSerialize, writer, settings);
			return;
		}

		_ = settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		JsonSerializer.Serialize(writer, toSerialize, options);
	}

	public static T DeserializeParams<T>(ref Utf8JsonReader reader, IElasticsearchClientSettings settings)
	{
		if (settings.Experimental.UseSourceSerializerForScriptParameters)
		{
			var result = Deserialize<T>(ref reader, settings);
			return result;
		}

		_ = settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		return JsonSerializer.Deserialize<T>(ref reader, options);
	}

	public static void Serialize<T>(T toSerialize, Utf8JsonWriter writer, IElasticsearchClientSettings settings) =>
		Serialize<T>(toSerialize, writer, settings.SourceSerializer);

	public static void Serialize(object toSerialize, Type type, Utf8JsonWriter writer, IElasticsearchClientSettings settings) =>
		Serialize(toSerialize, type, writer, settings.SourceSerializer);

	public static void Serialize<T>(T toSerialize, Utf8JsonWriter writer, Serializer sourceSerializer)
	{
		if (sourceSerializer is DefaultSourceSerializer defaultSerializer)
		{
			// When the serializer is our own, which uses STJ we can avoid unnecessary allocations and serialize straight into the writer
			// In most cases this is not the case as it's wrapped in the DiagnosticsSerializerProxy
			// Ideally, we'd short-circuit here if we know there are no listeners or avoid wrapping the default source serializer.
			JsonSerializer.Serialize(writer, toSerialize, defaultSerializer.Options);
			return;
		}

		// We may be using a custom serializer or most likely, we're registered via DiagnosticsSerializerProxy
		// We cannot use STJ since the implementation may use another serializer.
		// This path is a little less optimal
		using var ms = new MemoryStream();
		sourceSerializer.Serialize(toSerialize, ms);
		ms.Position = 0;
#if NET6_0_OR_GREATER
		writer.WriteRawValue(ms.GetBuffer().AsSpan()[..(int)ms.Length], true);
#else
		// This is not super efficient but a variant on the suggestion at https://github.com/dotnet/runtime/issues/1784#issuecomment-608331125
		using var document = JsonDocument.Parse(ms);
		document.RootElement.WriteTo(writer);
#endif
	}

	public static void Serialize(object toSerialize, Type type, Utf8JsonWriter writer, Serializer sourceSerializer)
	{
		if (sourceSerializer is DefaultSourceSerializer defaultSerializer)
		{
			// When the serializer is our own, which uses STJ we can avoid unnecessary allocations and serialize straight into the writer
			// In most cases this is not the case as it's wrapped in the DiagnosticsSerializerProxy
			// Ideally, we'd short-circuit here if we know there are no listeners or avoid wrapping the default source serializer.
			JsonSerializer.Serialize(writer, toSerialize, type, defaultSerializer.Options);
			return;
		}

		// We may be using a custom serializer or most likely, we're registered via DiagnosticsSerializerProxy
		// We cannot use STJ since the implementation may use another serializer.
		// This path is a little less optimal
		using var ms = new MemoryStream();
		sourceSerializer.Serialize(toSerialize, ms);
		ms.Position = 0;
#if NET6_0_OR_GREATER
		writer.WriteRawValue(ms.GetBuffer().AsSpan()[..(int)ms.Length], true);
#else
		// This is not super efficient but a variant on the suggestion at https://github.com/dotnet/runtime/issues/1784#issuecomment-608331125
		using var document = JsonDocument.Parse(ms);
		document.RootElement.WriteTo(writer);
#endif
	}

	public static T Deserialize<T>(ref Utf8JsonReader reader, IElasticsearchClientSettings settings)
	{
		var sourceSerializer = settings.SourceSerializer;

		if (sourceSerializer is DefaultSourceSerializer defaultSerializer)
		{
			// When the serializer is our own which uses STJ we can avoid unnecessary allocations and serialize straight into the writer
			return JsonSerializer.Deserialize<T>(ref reader, defaultSerializer.Options);
		}
		else
		{
			// TODO: This allocates more than we'd like. Review this post alpha

			using var jsonDoc = JsonSerializer.Deserialize<JsonDocument>(ref reader);
			using var stream = settings.MemoryStreamFactory.Create();

			var writer = new Utf8JsonWriter(stream);
			jsonDoc.WriteTo(writer);
			writer.Flush();
			stream.Position = 0;

			return sourceSerializer.Deserialize<T>(stream);
		}
	}
}
