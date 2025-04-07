// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers.Text;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

#region DateTime

internal sealed class DateTimeMarker;

internal sealed class DateTimeMarkerConverter :
	JsonConverter<DateTimeMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public DateTimeMarkerConverter()
	{
		WrappedConverter = new DateTimeConverter();
	}

	public override DateTimeMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, DateTimeMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class DateTimeConverter :
	JsonConverter<DateTimeOffset>
{
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if ((reader.TokenType is JsonTokenType.String) && Utf8Parser.TryParse(reader.ValueSpan, out long timestamp, out var consumed) &&
			(consumed == reader.ValueSpan.Length))
		{
			// Leniency for stringified numbers.
			return DateTimeHelper.FromEpochMilliseconds(timestamp);
		}

		return reader.TokenType switch
		{
			JsonTokenType.String => ParseValue(ref reader),
			JsonTokenType.Number => DateTimeHelper.FromEpochMilliseconds(reader.GetInt64()),
			_ => throw new JsonException($"Expected JSON '{JsonTokenType.String}' or '{JsonTokenType.Number}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(DateTimeHelper.ToEpochMilliseconds(value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static DateTimeOffset ParseValue(ref Utf8JsonReader reader)
	{
		return DateTimeOffset.TryParse(reader.GetString()!, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result)
			? result
			: throw new JsonException($"Unable to convert JSON string value '{reader.GetString()!}' to '{nameof(DateTimeOffset)}'.");

		// TODO: https://github.com/dotnet/runtime/issues/28942#issuecomment-724161375

		//return Utf8Parser.TryParse(reader.ValueSpan, out DateTimeOffset result, out var consumed, 'O') && (consumed == reader.ValueSpan.Length)
		//	? result
		//	: throw new JsonException($"Unable to convert JSON string value '{reader.GetString()!}' to '{nameof(DateTimeOffset)}'.");
	}
}

#endregion DateTime

#region DateTimeSeconds

internal sealed class DateTimeSecondsMarker;

internal sealed class DateTimeSecondsMarkerConverter :
	JsonConverter<DateTimeSecondsMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public DateTimeSecondsMarkerConverter()
	{
		WrappedConverter = new DateTimeSecondsConverter();
	}

	public override DateTimeSecondsMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, DateTimeSecondsMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class DateTimeSecondsConverter :
	JsonConverter<DateTimeOffset>
{
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if ((reader.TokenType is JsonTokenType.String) && Utf8Parser.TryParse(reader.ValueSpan, out long timestamp, out var consumed) &&
			(consumed == reader.ValueSpan.Length))
		{
			// Leniency for stringified numbers.
			return DateTimeHelper.FromEpochSeconds(timestamp);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochSeconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(DateTimeHelper.ToEpochSeconds(value));
	}
}

#endregion DateTimeSeconds

#region DateTimeMillis

internal sealed class DateTimeMillisMarker;

internal sealed class DateTimeMillisMarkerConverter :
	JsonConverter<DateTimeMillisMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public DateTimeMillisMarkerConverter()
	{
		WrappedConverter = new DateTimeMillisConverter();
	}

	public override DateTimeMillisMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, DateTimeMillisMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class DateTimeMillisConverter :
	JsonConverter<DateTimeOffset>
{
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if ((reader.TokenType is JsonTokenType.String) && Utf8Parser.TryParse(reader.ValueSpan, out long timestamp, out var consumed) &&
			(consumed == reader.ValueSpan.Length))
		{
			// Leniency for stringified numbers.
			return DateTimeHelper.FromEpochMilliseconds(timestamp);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochMilliseconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(DateTimeHelper.ToEpochMilliseconds(value));
	}
}

#endregion DateTimeMillis

#region DateTimeNanos

internal sealed class DateTimeNanosMarker;

internal sealed class DateTimeNanosMarkerConverter :
	JsonConverter<DateTimeNanosMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public DateTimeNanosMarkerConverter()
	{
		WrappedConverter = new DateTimeNanosConverter();
	}

	public override DateTimeNanosMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, DateTimeNanosMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class DateTimeNanosConverter :
	JsonConverter<DateTimeOffset>
{
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if ((reader.TokenType is JsonTokenType.String) && Utf8Parser.TryParse(reader.ValueSpan, out long timestamp, out var consumed) &&
			(consumed == reader.ValueSpan.Length))
		{
			// Leniency for stringified numbers.
			return DateTimeHelper.FromEpochNanoseconds(timestamp);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochNanoseconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(DateTimeHelper.ToEpochNanoseconds(value));
	}
}

#endregion DateTimeNanos

#region DateTimeSecondsFloat

internal sealed class DateTimeSecondsFloatMarker;

internal sealed class DateTimeSecondsFloatMarkerConverter :
	JsonConverter<DateTimeSecondsFloatMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public DateTimeSecondsFloatMarkerConverter()
	{
		WrappedConverter = new DateTimeSecondsFloatConverter();
	}

	public override DateTimeSecondsFloatMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, DateTimeSecondsFloatMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class DateTimeSecondsFloatConverter :
	JsonConverter<DateTimeOffset>
{
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if ((reader.TokenType is JsonTokenType.String) && Utf8Parser.TryParse(reader.ValueSpan, out double timestamp, out var consumed) &&
			(consumed == reader.ValueSpan.Length))
		{
			// Leniency for stringified numbers.
			return DateTimeHelper.FromEpochSeconds((long)timestamp);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochSeconds((long)reader.GetDouble());
	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue((double)DateTimeHelper.ToEpochSeconds(value));
	}
}

#endregion DateTimeSecondsFloat

#region DateTimeMillisFloat

internal sealed class DateTimeMillisFloatMarker;

internal sealed class DateTimeMillisFloatMarkerConverter :
	JsonConverter<DateTimeMillisFloatMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public DateTimeMillisFloatMarkerConverter()
	{
		WrappedConverter = new DateTimeMillisFloatConverter();
	}

	public override DateTimeMillisFloatMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, DateTimeMillisFloatMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class DateTimeMillisFloatConverter :
	JsonConverter<DateTimeOffset>
{
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if ((reader.TokenType is JsonTokenType.String) && Utf8Parser.TryParse(reader.ValueSpan, out double timestamp, out var consumed) &&
			(consumed == reader.ValueSpan.Length))
		{
			// Leniency for stringified numbers.
			return DateTimeHelper.FromEpochMilliseconds((long)timestamp);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochMilliseconds((long)reader.GetDouble());
	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue((double)DateTimeHelper.ToEpochMilliseconds(value));
	}
}

#endregion DateTimeMillisFloat
