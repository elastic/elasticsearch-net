// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement.Json;

public sealed class IndexSettingsTimeSeriesConverter : JsonConverter<IndexSettingsTimeSeries>
{
	private static readonly JsonEncodedText PropEndTime = JsonEncodedText.Encode("end_time"u8);
	private static readonly JsonEncodedText PropStartTime = JsonEncodedText.Encode("start_time"u8);

	public override IndexSettingsTimeSeries Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.StartObject);
		LocalJsonValue<DateTimeOffset?> propEndTime = default;
		LocalJsonValue<DateTimeOffset?> propStartTime = default;
		while (reader.Read() && reader.TokenType is JsonTokenType.PropertyName)
		{
			if (propEndTime.TryReadProperty(ref reader, options, PropEndTime, static DateTimeOffset? (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadDateTime(ref r, o)))
			{
				continue;
			}

			if (propStartTime.TryReadProperty(ref reader, options, PropStartTime, static DateTimeOffset? (ref Utf8JsonReader r, JsonSerializerOptions o) => ReadDateTime(ref r, o)))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is JsonUnmappedMemberHandling.Skip)
			{
				reader.SafeSkip();
				continue;
			}

			throw new JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(JsonTokenType.EndObject);
		return new IndexSettingsTimeSeries(JsonConstructorSentinel.Instance)
		{
			EndTime = propEndTime.Value,
			StartTime = propStartTime.Value
		};
	}

	public override void Write(Utf8JsonWriter writer, IndexSettingsTimeSeries value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEndTime, value.EndTime, null, static (Utf8JsonWriter w, JsonSerializerOptions o, DateTimeOffset? v) => w.WriteNullableValueEx<DateTimeOffset>(o, v, typeof(DateTimeMarker)));
		writer.WriteProperty(options, PropStartTime, value.StartTime, null, static (Utf8JsonWriter w, JsonSerializerOptions o, DateTimeOffset? v) => w.WriteNullableValueEx<DateTimeOffset>(o, v, typeof(DateTimeMarker)));
		writer.WriteEndObject();
	}

	private static DateTimeOffset? ReadDateTime(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		// Detect ISO 8601-1:2019 extended year format (prefixed with '-' or '+') which cannot be
		// represented by DateTimeOffset. Clamp to the nearest representable value.
		if (reader.TokenType is JsonTokenType.String)
		{
			var span = reader.ValueSpan;
			if (span.Length > 0 && span[0] == (byte)'-')
				return DateTimeOffset.MinValue;
			if (span.Length > 0 && span[0] == (byte)'+')
				return DateTimeOffset.MaxValue;
		}

		return reader.ReadNullableValueEx<DateTimeOffset>(options, typeof(DateTimeMarker));
	}
}
