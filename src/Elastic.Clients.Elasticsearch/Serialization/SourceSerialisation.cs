// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

#if NET6_0_OR_GREATER
public struct RawJsonString
{
	public RawJsonString(string rawJson) => Json = rawJson;

	public string Json { get; init; }
}

internal class RawJsonConverter : JsonConverter<RawJsonString>
{
	public override RawJsonString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
	public override void Write(Utf8JsonWriter writer, RawJsonString value, JsonSerializerOptions options) => writer.WriteRawValue(value.Json);
}
#endif

/// <summary>
/// May be used by requests that need to serialise only part of their source rather than the request object itself.
/// </summary>
internal static class SourceSerialisation
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

	public static void DeserializeParams<T>(ref Utf8JsonReader reader, IElasticsearchClientSettings settings)
	{
		if (settings.Experimental.UseSourceSerializerForScriptParameters)
		{
			Deserialize<T>(ref reader, settings);
			return;
		}

		_ = settings.RequestResponseSerializer.TryGetJsonSerializerOptions(out var options);
		JsonSerializer.Deserialize<T>(ref reader, options);
	}

	public static void Serialize<T>(T toSerialize, Utf8JsonWriter writer, IElasticsearchClientSettings settings) =>
		Serialize<T>(toSerialize, writer, settings.SourceSerializer);

	public static void Serialize<T>(T toSerialize, Utf8JsonWriter writer, SerializerBase sourceSerializer)
	{
		if (sourceSerializer is DefaultSourceSerializer defaultSerializer)
		{
			// When the serializer is our own, which uses STJ we can avoid unneccesary allocations and serialise straight into the writer
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

	public static T Deserialize<T>(ref Utf8JsonReader reader, IElasticsearchClientSettings settings)
	{
		var sourceSerializer = settings.SourceSerializer;

		if (sourceSerializer is DefaultSourceSerializer defaultSerializer)
		{
			// When the serializer is our own which uses STJ we can avoid unneccesary allocations and serialise straight into the writer
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
