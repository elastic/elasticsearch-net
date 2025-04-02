// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport.Extensions;

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
	JsonConverter<DateTime>
{
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => ParseValue(ref reader),
			JsonTokenType.Number => DateTimeHelper.FromEpochMilliseconds(reader.GetInt64()),
			_ => throw new JsonException($"Expected JSON '{JsonTokenType.String}' or '{JsonTokenType.Number}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(DateTimeHelper.ToEpochMilliseconds(value));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static DateTime ParseValue(ref Utf8JsonReader reader)
	{
		Debug.Assert(!reader.HasValueSequence);

		return Utf8Parser.TryParse(reader.ValueSpan, out DateTime result, out var consumed) && (consumed == reader.ValueSpan.Length)
			? result
			: throw new JsonException($"Unable to convert JSON string value '{reader.GetString()!}' to '{nameof(DateTime)}'.");
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
	JsonConverter<DateTime>
{
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochSeconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
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
	JsonConverter<DateTime>
{
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochMilliseconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
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
	JsonConverter<DateTime>
{
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochNanoseconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
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
	JsonConverter<DateTime>
{
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochSeconds((long)reader.GetDouble());
	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
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
	JsonConverter<DateTime>
{
	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.Number);

		return DateTimeHelper.FromEpochMilliseconds((long)reader.GetDouble());
	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue((double)DateTimeHelper.ToEpochMilliseconds(value));
	}
}

#endregion DateTimeMillisFloat
