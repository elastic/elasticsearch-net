// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using System.Text.Json.Serialization;
using System.Text.Json;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class FieldValueConverter :
	JsonConverter<FieldValue>
{
	public override FieldValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		switch (reader.TokenType)
		{
			case JsonTokenType.Null:
				return FieldValue.Null;

			case JsonTokenType.String:
				return FieldValue.String(reader.GetString()!);

			case JsonTokenType.Number:
				if (reader.TryGetInt64(out var l))
				{
					return FieldValue.Long(l);
				}

				if (reader.TryGetDouble(out var d))
				{
					return FieldValue.Double(d);
				}

				throw new JsonException($"Unexpected number format while deserializing '{nameof(FieldValue)}'.");

			case JsonTokenType.True:
				return FieldValue.True;

			case JsonTokenType.False:
				return FieldValue.False;
		}

		throw new JsonException($"Unexpected token type '{reader.TokenType}' read while deserializing '{nameof(FieldValue)}'.");
	}

	public override void Write(Utf8JsonWriter writer, FieldValue value, JsonSerializerOptions options)
	{
		if (value.TryGetString(out var stringValue))
		{
			writer.WriteValue(options, stringValue);
		}
		else if (value.TryGetBool(out var boolValue))
		{
			writer.WriteValue(options, boolValue.Value);
		}
		else if (value.TryGetLong(out var longValue))
		{
			writer.WriteValue(options, longValue.Value);
		}
		else if (value.TryGetDouble(out var doubleValue))
		{
			writer.WriteValue(options, doubleValue.Value);
		}
		else if (value.Kind is FieldValue.ValueKind.Null)
		{
			writer.WriteNullValue();
		}
		else
		{
			throw new JsonException($"ValueKind '{value.Kind}' is not supported. This is likely a bug and should be reported as an issue.");
		}
	}
}
